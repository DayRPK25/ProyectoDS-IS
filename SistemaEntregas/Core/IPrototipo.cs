namespace SistemaEntregas.Core
{
    /// <summary>
    /// Interfaz del patrón Prototype. Todo objeto clonable debe implementarla.
    /// </summary>
    public interface IPrototipo
    {
        IPrototipo Clonar();
    }
}
