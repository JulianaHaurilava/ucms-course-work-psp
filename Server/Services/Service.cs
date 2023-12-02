namespace Server.Services
{
	public abstract class Service<T>
	{
		public abstract List<T> GetAll();
		public abstract T Get(int id);
		public abstract void Upsert(T item);
		public abstract void Remove(T item);
	}
}
