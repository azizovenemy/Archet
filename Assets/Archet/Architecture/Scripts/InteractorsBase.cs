using System;
using System.Collections.Generic;

namespace Architecture
{
    public class InteractorsBase
    {
        private Dictionary<Type, Interactor> interactorsMap;
        private SceneConfig sceneConfig;

        public InteractorsBase(SceneConfig sceneConfig)
        {
            this.sceneConfig = sceneConfig;
        }

        public void CreateAllInteractors()
        {
            this.interactorsMap = this.sceneConfig.CreateAllInteractors();
        }

        public void SendOnCreateToAllInteractors()
        {
            var allInteractor = this.interactorsMap.Values;
            foreach(var interactor in allInteractor)
            {
                interactor.OnCreate();
            }
        }

        public void InitializeAllInteractors()
        {
            var allInteractor = this.interactorsMap.Values;
            foreach (var interactor in allInteractor)
            {
                interactor.Initialize();
            }
        }

        public void SendOnStartToAllInteractors()
        {
            var allInteractor = this.interactorsMap.Values;
            foreach (var interactor in allInteractor)
            {
                interactor.OnStart();
            }
        }

        public T GetInteractor<T>() where T : Interactor
        {
            var type = typeof(T);
            return (T)this.interactorsMap[type];
        }
    }
}

