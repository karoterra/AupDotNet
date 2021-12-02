namespace Karoterra.AupDotNet.ExEdit
{
    public class CheckboxDefinition
    {
        public string Name { get; set; }
        public bool IsCheckbox { get; set; }
        public int Default { get; set; }

        public CheckboxDefinition(string name, bool isCheckbox, int def)
        {
            Name = name;
            IsCheckbox = isCheckbox;
            Default = def;
        }
    }
}
