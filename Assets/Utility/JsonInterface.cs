using System.Collections;

abstract public class JsonInterface 
{	
	public JsonInterface() { }
	public JsonInterface(Hashtable hashTable)
	{
		if (hashTable != null) FromJson(hashTable);
	}

	public abstract void ToJsonObject(Hashtable hashTable);
	public abstract void FromJson(Hashtable hashTable);

	public virtual string ToJsonString()
	{
		Hashtable hashTable = new Hashtable();
		ToJsonObject(hashTable);
		return MiniJsonExtensions.toJson(hashTable);
	}

	public virtual void FromJson(string json)
	{
		Hashtable hashTable = MiniJsonExtensions.hashtableFromJson(json);
		FromJson(hashTable);
	}
}