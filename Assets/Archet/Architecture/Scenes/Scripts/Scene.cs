using Architecture;
using System.Collections;
using UnityEngine;

public class Scene
{
    private InteractorsBase interactorsBase;
    private RepositoriesBase repositoriesBase;
    private SceneConfig sceneConfig;

    public Scene(SceneConfig config)
    {
        this.sceneConfig = config;
        this.interactorsBase = new InteractorsBase(config);
        this.repositoriesBase = new RepositoriesBase(config);
    }

    public Coroutine InitializeAsync() 
    {
        return Coroutines.StartRoutine(this.InitializeRoutine());
    }

    private IEnumerator InitializeRoutine()
    {
        repositoriesBase.CreateAllRepositories();
        interactorsBase.CreateAllInteractors();
        yield return null;

        repositoriesBase.SendOnCreateToAllRepositories();
        interactorsBase.SendOnCreateToAllInteractors();
        yield return null;

        repositoriesBase.InitializeAllRepositories();
        interactorsBase.InitializeAllInteractors();
        yield return null;

        repositoriesBase.SendOnStartToAllRepositories();
        interactorsBase.SendOnStartToAllInteractors();
    }

    public T GetRepository<T>() where T : Repository
    {
        return this.repositoriesBase.GetRepository<T>();
    }

    public T GetInteractor<T>() where T : Interactor
    {
        return this.interactorsBase.GetInteractor<T>();
    }
}
