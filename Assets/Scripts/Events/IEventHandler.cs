  public interface IEventHandler<T> where T : IGameEvent
  {
    void Handle(T eventData);
  }