using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;
using UnityEngine.Sprites;
using System.IO;
using System;

namespace VolcanoidsModdingFramework
{
    public class Framework : MonoBehaviour
    {
        private void Awake()
        {
            Debug.Log("Module: " + GetType().Name + " loaded successfully");
            CreateFramework();
            Debug.Log("Module: " + GetType().Name + " Initialized successfully");
        }
        private void CreateFramework()
        {
            haserror = false;
            depositsurface = Resources.FindObjectsOfTypeAll<DepositLocationSurface>();
            depositunderground = Resources.FindObjectsOfTypeAll<DepositLocationUnderground>();
        }
        public static void Initialize<T>(ref T str)
    where T : struct, ISerializationCallbackReceiver
        {
            str.OnAfterDeserialize();
        }
        public Sprite Sprite2(string iconpath)
        {
            var path = Path.Combine(Application.persistentDataPath, "Mods", iconpath);
            if (!File.Exists(path))
            {
                Debug.LogError("Specified Icon path not found: " + path);
                haserror = true;
                return null;
            }
            var bytes = File.ReadAllBytes(path);


            var texture = new Texture2D(512, 512, TextureFormat.ARGB32, true);
            texture.LoadImage(bytes);

            var sprite = Sprite.Create(texture, new Rect(Vector2.zero, Vector2.one * texture.width), new Vector2(0.5f, 0.5f), texture.width, 0, SpriteMeshType.FullRect, Vector4.zero, false);
            return sprite;
        }
        public void CreateItemTracks(string codename, int surfacemovementspeed, int undergroundmovementspeed, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainTracksItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SurfaceMovementSpeed = surfacemovementspeed;
            item.UndergroundMovementSpeed = undergroundmovementspeed;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemHull(string codename, float damageperdegree, float armorbonus, float temperatureflow, float temperature, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainHullItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.Temperature = temperature;
            item.TemperatureFlow = temperatureflow;
            item.ArmorBonus = armorbonus;
            item.DamagePerDegree = damageperdegree;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemDrill(string codename, float armorbonus, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);
            var materials = GameResources.Instance.CellMaterials.ToArray();

            var item = ScriptableObject.CreateInstance<TrainDrillItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.ArmorBonus = armorbonus;
            item.Materials = materials;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemEngine(string codename, int segmentcount, int mincoreslotcount, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainEngineItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SegmentCount = segmentcount;
            item.MinimumCoreSlotCount = mincoreslotcount;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemCore(string codename, int slotcount, int maxenergy, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<TrainCoreItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.SlotCount = slotcount;
            item.MaxEnergy = maxenergy;
            item.Icon = icon;
            item.MaxStack = maxstack;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItem(string codename, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string recipecategoryname, Sprite icon)
        {
            var recipecategory = GameResources.Instance.Items.FirstOrDefault(s => s.name == recipecategoryname);

            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = recipecategory.Category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateRecipe(InventoryItemData[] Inputs, InventoryItemData Output, string guidstring, Recipe recipecategory, string name, float ProductionTimeMultiplier, Sprite icon)
        {
            var newrecipe = ScriptableObject.CreateInstance<Recipe>();
            newrecipe.name = name;
            newrecipe.Order = recipecategory.Order;
            newrecipe.Inputs = Inputs;
            newrecipe.Output = Output;
            newrecipe.Icon = icon;
            newrecipe.RequiredUpgrades = recipecategory.RequiredUpgrades;
            newrecipe.Categories = recipecategory.Categories.ToArray();
            newrecipe.ProductionTime = recipecategory.ProductionTime * ProductionTimeMultiplier;

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(newrecipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = newrecipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public ItemDefinition GetItem(string itemname)
        {
            ItemDefinition item = GameResources.Instance.Items.FirstOrDefault(s => s.name == itemname);
            if (item == null)
            {
                Debug.LogError("Item is null, name: " + itemname + ". Replacing with NullItem");
                haserror = true;
                return GameResources.Instance.Items.FirstOrDefault(s => s.name == "NullItem");
            }
            return item;

        }
        public Recipe GetRecipe(string recipename)
        {
            var recipe = GameResources.Instance.Recipes.FirstOrDefault(s => s.name == recipename);
            if (recipe == null)
            {
                Debug.LogError("Specified Recipe not found: " + recipename);
                haserror = true;
                return GameResources.Instance.Recipes.FirstOrDefault(s => s.name == "NullItem");
            }
            return recipe;
        }
        public InventoryItemData CreateSingleIID(string itemname, int amount)
        {
            return new InventoryItemData { Item = GetItem(itemname), Amount = amount };
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            NewNoInputRecipe(
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void CreateRecipeSimple(string Input1name, int Input1amount, string Input2name, int Input2amount, string Input3name, int Input3amount, string Input4name, int Input4amount, string Input5name, int Input5amount, string GUID, string Output, int Outputamount, string recipecategory, string RecipeName, float ProductionTimeMultiplier, Sprite icon)
        {
            CreateRecipe(new InventoryItemData[] { CreateSingleIID(Input1name, Input1amount),
            CreateSingleIID(Input2name, Input2amount), CreateSingleIID(Input3name, Input3amount), CreateSingleIID(Input4name, Input4amount), CreateSingleIID(Input5name, Input5amount) },
            CreateSingleIID(Output, Outputamount), GUID,
            GetRecipe(recipecategory), RecipeName, ProductionTimeMultiplier, icon);
        }
        public void NewNoInputRecipe(InventoryItemData Output, string guidstring, Recipe recipecategory, string name, float ProductionTimeMultiplier, Sprite icon)
        {
            var newrecipe = ScriptableObject.CreateInstance<Recipe>();
            newrecipe.name = name;
            newrecipe.Order = recipecategory.Order;
            newrecipe.Inputs = new InventoryItemData[0];
            newrecipe.Output = Output;
            newrecipe.Icon = icon;
            newrecipe.RequiredUpgrades = recipecategory.RequiredUpgrades;
            newrecipe.Categories = recipecategory.Categories.ToArray();
            newrecipe.ProductionTime = recipecategory.ProductionTime * ProductionTimeMultiplier;

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(newrecipe, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = newrecipe, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateItemModuleTurret(string codename, string variantname, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, Sprite icon, int aimspeed, int damagemultiplier, int rangemultiplier, int rateoffiremultiplier, int effectiverangemultiplier)
        {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon = icon;
            var prefabParent = new GameObject();
            var olditem = GameResources.Instance.Items.FirstOrDefault(s => s.name == "TurretModule");
            prefabParent.SetActive(false);
            var turretStrong = Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var turretComponent = turretStrong.GetComponentInChildren<Turret>();
            var turretWeapon = turretStrong.GetComponentInChildren<Weapon>();
            var reloader = turretStrong.GetComponentInChildren<WeaponReloaderNoAmmo>();
            var ammoStats = reloader.LoadedAmmo;
            turretComponent.m_pitchSpeed *= aimspeed;
            turretComponent.m_yawSpeed *= aimspeed;
            ammoStats.Damage *= damagemultiplier;
            ammoStats.Range *= rangemultiplier;
            ammoStats.RateOfFire *= rateoffiremultiplier;
            ammoStats.EffectiveRange *= effectiverangemultiplier;
            turretStrong.GetComponent<GridModule>().VariantName = variantname;
            turretStrong.GetComponent<GridModule>().Item = item;
            item.Prefabs = new GameObject[] { turretStrong };


            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(WeaponReloaderNoAmmo).GetField("m_stats", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(reloader, ammoStats);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public RecipeCategory Findcategories(string categoryname)
        {
            tempcategory = null;
            foreach (Recipe recipe in GameResources.Instance.Recipes)
            {
                foreach (RecipeCategory category in recipe.Categories)
                {
                    if (category.name == categoryname)
                    {
                        tempcategory = category;
                    }
                }
            }
            return tempcategory;
        }
        public void CreateItemModuleProduction(string codename, string variantname, int maxstack, LocalizedString name, LocalizedString desc, string guidstring, string categoryname, string factorytypename, Sprite icon, RecipeCategory[] categories)
        {
            var category = GameResources.Instance.Items.FirstOrDefault(s => s.name == categoryname).Category;
            var item = ScriptableObject.CreateInstance<ItemDefinition>();
            item.name = codename;
            item.Category = category;
            item.MaxStack = maxstack;
            item.Icon = icon;

            var prefabParent = new GameObject();
            var olditem = GameResources.Instance.Items.FirstOrDefault(s => s.name == "ProductionModuleT3");
            var factorytype = GameResources.Instance.FactoryTypes.FirstOrDefault(s => s.name == factorytypename);
            prefabParent.SetActive(false);
            var newmodule = Instantiate(olditem.Prefabs[0], prefabParent.transform);
            var module = newmodule.GetComponentInChildren<ProductionModule>();
            var gridmodule = newmodule.GetComponent<GridModule>();
            gridmodule.VariantName = variantname;
            gridmodule.Item = item;
            item.Prefabs = new GameObject[] { newmodule };
            var modulecategory = RuntimeAssetCacheLookup.Get<ModuleCategory>().First(s => s.name == factorytypename);
            modulecategory.Modules = modulecategory.Modules.Concat(new ItemDefinition[] { item }).ToArray();

            LocalizedString nameStr = name;
            LocalizedString descStr = desc;
            Initialize(ref nameStr);
            Initialize(ref descStr);

            typeof(ProductionModule).GetField("m_factoryType", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, factorytype);
            typeof(ProductionModule).GetField("m_module", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, gridmodule);
            typeof(ProductionModule).GetField("m_categories", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(module, categories);
            typeof(ItemDefinition).GetField("m_name", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, nameStr);
            typeof(ItemDefinition).GetField("m_description", BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, descStr);

            var guid = GUID.Parse(guidstring);

            typeof(Definition).GetField("m_assetId", BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance).SetValue(item, guid);

            AssetReference[] assets = new AssetReference[] { new AssetReference() { Object = item, Guid = guid, Labels = new string[0] } };
            RuntimeAssetStorage.Add(assets, default);
        }
        public void CreateDeposit(bool Underground, int PercentageToReplace, string outputname, float minyield, float maxyield, string ItemToReplace)
        {

            if (Underground)
            {
                foreach (DepositLocationUnderground underground in depositunderground)
                {
                    if (UnityEngine.Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && underground.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            underground.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(underground, GetItem(outputname));
                        }
                    }
                }
            }
            if (!Underground)
            {
                foreach (DepositLocationSurface surface in depositsurface)
                {
                    if (UnityEngine.Random.Range(0, 100) <= PercentageToReplace)
                    {
                        if ((ItemToReplace != null && surface.Ore == GetItem(ItemToReplace)) || ItemToReplace == null)
                        {
                            surface.Yield = UnityEngine.Random.Range(minyield, maxyield);
                            OreField.SetValue(surface, GetItem(outputname));
                        }
                    }
                }
            }
        }
        static readonly FieldInfo OreField = typeof(DepositLocation).GetField("m_ore", BindingFlags.NonPublic | BindingFlags.Instance);
        private DepositLocationSurface[] depositsurface;
        private DepositLocationUnderground[] depositunderground;
        private RecipeCategory tempcategory;
        public bool haserror;
    }
}
