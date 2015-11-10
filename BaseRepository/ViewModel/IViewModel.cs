namespace BaseRepository.ViewModel
{
    public interface IViewModel<TModelo> 
        where TModelo : class 
    {
        TModelo ToBaseDatos();

        void FromBaseDatos(TModelo modelo);

        void UpdateBaseDatos(TModelo modelo);

        object[] GetKeys();
    }
}
