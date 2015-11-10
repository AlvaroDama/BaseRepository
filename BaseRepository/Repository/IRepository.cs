using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using BaseRepository.ViewModel;

namespace BaseRepository.Repository
{
    public interface IRepository<TModel, TViewModel> where TModel:class 
                                              where TViewModel: IViewModel<TModel>
    {

        TViewModel Add(TViewModel model);

        int Borrar(TViewModel model);

        int Borrar(Expression<Func<TModel, bool>> expression);

        int Actualizar(TViewModel model);

        ICollection<TViewModel> Get();

        TViewModel Get(params object[] keys); //el params construye el array de objetos separado por comas como si fuesen parámetros de la función

        ICollection<TViewModel> Get(Expression<Func<TModel, bool>> expression);

    }
}
