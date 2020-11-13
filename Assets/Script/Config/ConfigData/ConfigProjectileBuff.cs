using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[System.Serializable]
public class ConfigProjectileBuffRecord
{
    [SerializeField]
    private int id = 0;
    [SerializeField]
    private string idSequences = "";
    [SerializeField]
    private string sprite = "";

    public List<int> IdSequences { get => MyUltis.StringToIntegerList(idSequences);}
    public string Sprite { get => sprite;}
    public int Id { get => id; }
}

public class ConfigProjectileBuff :BYDataTable<ConfigProjectileBuffRecord>
{
    public override void SetCompareObject()
    {
        recoreCompare = new ConfigCompareKey<ConfigProjectileBuffRecord>("id");
    }

    public ConfigProjectileBuffRecord GetByIdSequences(List<int> input)
    {
        if(input.Count <= 0) return records.Where(r => r.Id == 1).First();

        var result = from s in records
                where s.IdSequences.Intersect(input).Count() == input.Count()
                select s;
        if (result.Count() <= 0)
        {
            return records.Where(r => r.Id == 1).First();
        }
        else return result.First();
    }
}
