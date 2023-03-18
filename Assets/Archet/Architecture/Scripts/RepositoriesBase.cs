using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Architecture
{
    public class RepositoriesBase : MonoBehaviour
    {
        private Dictionary<Type, Repository> repositoriesMap;
        private SceneConfig sceneConfig;

        public RepositoriesBase(SceneConfig sceneConfig)
        {
            this.sceneConfig = sceneConfig;
        }

        public void CreateAllRepositories()
        {
            this.repositoriesMap = this.sceneConfig.CreateAllRepositories();
        }

        public void SendOnCreateToAllRepositories()
        {
            var allInteractor = this.repositoriesMap.Values;
            foreach (var interactor in allInteractor)
            {
                interactor.OnCreate();
            }
        }

        public void InitializeAllRepositories()
        {
            var allInteractor = this.repositoriesMap.Values;
            foreach (var interactor in allInteractor)
            {
                interactor.Initialize();
            }
        }

        public void SendOnStartToAllRepositories()
        {
            var allInteractor = this.repositoriesMap.Values;
            foreach (var interactor in allInteractor)
            {
                interactor.OnStart();
            }
        }

        public T GetRepository<T>() where T : Repository
        {
            var type = typeof(T);
            return (T)this.repositoriesMap[type];
        }
    }
}

