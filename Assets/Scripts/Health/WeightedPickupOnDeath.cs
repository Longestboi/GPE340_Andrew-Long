using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

[Serializable]
public class WeightedPickupOnDeath : Death
{
    #region Fields
    /// <summary>table that holds the drops</summary>
    public DropTable table;
    #endregion Fields

    #region MonoBehaviour
    // Start is called before the first frame update
    public override void Start()
    {
        // Call the base classes start function
        base.Start();
        
        // Calculate the CD
        if (table != null)
            table.CalculateCumulativeDensity();
    }
    #endregion MonoBehaviour

    #region Death
    /// <summary>Function that gets called when the pawn dies</summary>
    public override void OnDie()
    {
        table.SpawnRandomItem(transform);
    }
    #endregion Death
}

[Serializable]
public class DropTable {
    #region Fields
    [SerializeField]
    /// <summary>List of all droppable items</summary>
    private List<DropTableItem> tableItems;

    /// <summary>List of the cumulative weights</summary>
    private List<int> cumulativeDensityWeights;
    #endregion Fields

    #region Properties
    /// <summary>The last weight from the CDW list</summary>
    public int lastWeight {
        get { return cumulativeDensityWeights.LastOrDefault(); }
    }
    
    /// <summary>Allows for the indexing of the table through this class</summary>
    public DropTableItem this[int index] {
        get { return tableItems[index]; }
    }
    #endregion Properties

    #region WeightedPickupTable
    /// <summary>Add table item to the drop table</summary>
    /// <param name="to_add">The DropTableItem to be added</param>
    public void Add(DropTableItem to_add) 
    {
        // Add to table
        tableItems.Add(to_add);
        // Recalculate the CD if adding a new thing to the list
        CalculateCumulativeDensity();
    }

    /// <summary>Get a random item from the table</summary>
    /// <returns>GameObject prefab from the table item or null </returns>
    public GameObject GetRandomItemFromTable()
    {
        // Get random in range of weights
        int random = UnityEngine.Random.Range(0, lastWeight);

        // Loop through cumulative weights
        // and if our random is in that range, return the table item
        for (int i = 0; i < cumulativeDensityWeights.Count; i++)
            if (random < cumulativeDensityWeights[i])
                return tableItems[i].pickupPrefab;

       return null;
    }

    /// <summary>Spawn a random item at a specified transform</summary>
    /// <param name="transform">Transform the item will be spawned at</param>
    /// <returns>Instanciated GameObject or null</returns>
    public GameObject SpawnRandomItem(Transform transform) 
    {
        // Get a random item from the tables
        var rand = GetRandomItemFromTable();

        // If it's not null, instance the prefab and return it
        if (rand)
            return GameObject.Instantiate(
                rand,
                transform.position + rand.transform.position,
                transform.rotation
            );

        // Return null if nothing happened
        return null;
    }

    /// <summary>Calculate the cumulative density of all table items</summary>
    public void CalculateCumulativeDensity()
    {
        // Start list with the first item in table
        cumulativeDensityWeights = new List<int>{ tableItems[0].pickupWeight };

        // Create iter, and skip the first item
        var iter = tableItems.Skip(1);

        // Iterate over iter, and accumulate weights
        foreach (var i in iter)
            cumulativeDensityWeights.Add(cumulativeDensityWeights.Last() + i.pickupWeight);
    }
    #endregion WeightedPickupTable
}

[Serializable]
public struct DropTableItem {
    /// <summary>The object to be spawned</summary>
    public GameObject pickupPrefab;
    /// <summary>How likely the object is to spawn</summary>
    public int pickupWeight;
}