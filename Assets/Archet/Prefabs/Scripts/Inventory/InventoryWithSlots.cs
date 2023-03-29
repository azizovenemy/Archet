using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class InventoryWithSlots : IInventory
{
    public Action<object, IInventoryItem, int> OnInventoryItemAddedEvent;
    public Action<object, Type, int> OnInventoryItemRemovedEvent;
    public Action<object> OnInventoryStateChangedEvent;

    public int capacity { get ; set; }

    public bool isFull => _slots.All(slot => slot.isFull);

    private List<IInventorySlot> _slots;

    public InventoryWithSlots(int capacity)
    {
        this.capacity = capacity;

        _slots = new List<IInventorySlot>(capacity);
        
        for(int i = 0; i < capacity; i++)
            _slots.Add(new InventorySlot());
    }

    public bool TryToAdd(object sender, IInventoryItem item)
    {
        var slotsWithSameItemButNotEmpty = _slots.Find(slot => !slot.isEmpty && slot.itemType == item.type && !slot.isFull);

        if(slotsWithSameItemButNotEmpty != null)
            return TryToAddToSlot(sender, slotsWithSameItemButNotEmpty, item);

        var emptySlot = _slots.Find(slot => slot.isEmpty);

        if(emptySlot != null)
            return TryToAddToSlot(sender, emptySlot, item);

        Debug.Log($"Cannot add item {item.type}, amount {item.state.amount}, there is no place for that.");
        return false;
    }

    public bool TryToAddToSlot(object sender, IInventorySlot slot, IInventoryItem item)
    {
        var fits = slot.amount + item.state.amount <= item.info.maxItemsInInventorySlot;
        var amountToAdd = fits ? item.state.amount : item.info.maxItemsInInventorySlot - slot.amount;
        var amountLeft = item.state.amount - amountToAdd;
        var clonedItem = item.Clone();

        if (slot.isEmpty)
            slot.SetItem(clonedItem);
        else
            slot.item.state.amount += amountLeft;

        //Debug.Log($"Item added to inventory. ItemType: {item.type}, amount: {amountToAdd}");
        OnInventoryItemAddedEvent?.Invoke(sender, item, amountToAdd);
        OnInventoryStateChangedEvent?.Invoke(sender);

        if (amountLeft <= 0)
            return true;

        item.state.amount = amountLeft;
   
        return TryToAdd(sender, item);
    }
    
    public bool TryToRemove(object sender, Type itemType, int amount = 1)
    {
        var slotsWithItem = GetAllSlots(itemType);
        if(slotsWithItem.Length == 0)
            return false;

        int amountToRemove = amount;
        int count = slotsWithItem.Length;

        for(int i = count - 1; i >= 0; i--)
        {
            var slot = slotsWithItem[i];
            if(slot.amount >= amountToRemove)
            {
                slot.item.state.amount -= amountToRemove;

                if(slot.amount <= 0)
                    slot.Clear();

                Debug.Log($"Item removed from inventory. ItemType: {itemType}, amount: {amountToRemove}");
                OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountToRemove);
                OnInventoryStateChangedEvent?.Invoke(sender);
                break;
            }
            var amountRemoved = slot.amount;
            amountToRemove -= amountRemoved;
            slot.Clear();   
            Debug.Log($"Item removed from inventory. ItemType: {itemType}, amount: {amountRemoved}");
            OnInventoryItemRemovedEvent?.Invoke(sender, itemType, amountRemoved);
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        return true;
    }

    public bool HasItem(Type itemType, out IInventoryItem item)
    {
        item = GetItem(itemType);
        return item != null;
    }
    
    public IInventoryItem GetItem(Type itemType)
    {
        return _slots.Find(slot => slot.itemType == itemType).item;
    }

    public int GetItemAmount(Type itemType)
    {
        int amount = 0;
        var allItemSlots = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

        foreach (var slot in allItemSlots)
            amount += slot.amount;

        return amount;
    }

    public IInventoryItem[] GetAllItems()
    {
        var allItems = new List<IInventoryItem>();

        foreach(var slot in _slots)
            if(!slot.isEmpty)
                allItems.Add(slot.item);

        return allItems.ToArray();
    }

    public IInventoryItem[] GetAllItems(Type itemType)
    {
        var allItemsOfType = new List<IInventoryItem>();
        var slotsOfType = _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType);

        foreach (var slot in slotsOfType)
            allItemsOfType.Add(slot.item);

        return allItemsOfType.ToArray();
    }

    public void TransitFromSlotToSlot(object sender, IInventorySlot fromSlot, IInventorySlot toSlot)
    {
        //слот из которого пытаемся пернести пустой
        //if (fromSlot.isEmpty) 
            //return;

        //слот в который пытаемся перенести полный
        if (toSlot.isFull)
            return;

        //если слот в который пытаемся перенести не пустой,
        //но тип переносимого предмета отличается от уже лежащего в слоте
        //нельзя стакнуть
        if (!toSlot.isEmpty && fromSlot.item.info.title != toSlot.item.info.title)
            return;

        //если пытаемся перенести в тот же слот
        if (fromSlot == toSlot)
            return;

        //если слот в который пытаемся перенести пустой, выполняем пернос       
        if (toSlot.isEmpty)
        {
            toSlot.SetItem(fromSlot.item);
            fromSlot.Clear();
            OnInventoryStateChangedEvent?.Invoke(sender);
        }

        int slotCapacity = fromSlot.capacity;
        var fits = fromSlot.amount + toSlot.amount <= slotCapacity;
        int amountToAdd = fits ? fromSlot.amount : slotCapacity - toSlot.amount;
        int amountLeft = fromSlot.amount - amountToAdd;

        //изменение кол-ва предмета в слоте
        toSlot.item.state.amount += amountToAdd;
        
        //после переноса нет остатка, отчистить слот,
        //иначе оставить в слоте разницу его вместимости
        //и количества предметов пернесенных в другой слот
        if (fits)
            fromSlot.Clear();
        else
            fromSlot.item.state.amount = amountLeft;

        OnInventoryStateChangedEvent?.Invoke(sender);
    }

    public IInventoryItem[] GetEquippedItems()
    {
        var equippedItems = new List<IInventoryItem>();
        var requiredSlots = _slots.FindAll(slot => !slot.isEmpty && slot.item.state.isEquipped);

        foreach (var slot in requiredSlots)
            equippedItems.Add(slot.item);

        return equippedItems.ToArray();
    }

    public IInventorySlot[] GetAllEmptySlots()
    {
        return _slots.FindAll(slot => slot.isEmpty).ToArray();
    }

    public IInventorySlot[] GetAllSlots()
    {
        return _slots.ToArray();
    }

    public IInventorySlot[] GetAllSlots(Type itemType)
    {
        return _slots.FindAll(slot => !slot.isEmpty && slot.itemType == itemType).ToArray();
    }
}
