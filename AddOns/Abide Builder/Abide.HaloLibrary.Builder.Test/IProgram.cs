namespace Abide.HaloLibrary.Builder.Test
{
    public interface IProgram
    {
        void Start();
        void Exit();
        void OnInput(string input, params string[] args);
    }
}
