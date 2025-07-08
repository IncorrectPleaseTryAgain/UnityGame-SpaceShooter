public interface ISystem
{
    // Fix for CS1002, CS1520, CS0501, and CS0526:
    // Correctly define the method signature in the interface.
    public System.Collections.IEnumerator Initialize();
}