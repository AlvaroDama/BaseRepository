using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using BaseRepository.ViewModel;

namespace BaseRepository.Repository
{
    public class RepositoryEntity<TModel, TEntidad> : IRepository<TModel, TEntidad> 
        where TModel:class
        where TEntidad: IViewModel<TModel>, new ()

    {

        private DbContext context;

        protected DbSet<TModel> DbSet
        {
            get { return context.Set<TModel>(); }
        }

        public RepositoryEntity(DbContext ctx)
        {
            this.context = ctx;
        } 

        public TEntidad Add(TEntidad model)
        {
            var m = model.ToBaseDatos();
            DbSet.Add(m);


            try
            {
                context.SaveChanges();
                model.FromBaseDatos(m);
                return model;
            }
            catch (Exception)
            {
                
                return default(TEntidad);
            }
        }

        public int Actualizar(TEntidad model)
        {
            var obj = DbSet.Find(model.GetKeys());
            model.UpdateBaseDatos(obj);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }
        }

        public int Borrar(TEntidad model)
        {
            var obj = DbSet.Find(model.GetKeys());
            DbSet.Remove(obj);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {
                return 0;
            }

        }

        public int Borrar(Expression<Func<TModel, bool>> expression)
        {
            var data = DbSet.Where(expression);
            DbSet.RemoveRange(data);

            try
            {
                return context.SaveChanges();
            }
            catch (Exception)
            {

                return 0;
            }
        }

        public ICollection<TEntidad> Get()
        {
            var dat = new List<TEntidad>();

            foreach (var mod in DbSet)
            {
                var obj = new TEntidad();
                obj.FromBaseDatos(mod);
                dat.Add(obj);
            }

            return dat;
        }

        public TEntidad Get(params object[] keys)
        {
            var dato = DbSet.Find(keys);
            var retorno = new TEntidad();
            
            retorno.FromBaseDatos(dato);
            return retorno;
        }

        public ICollection<TEntidad> Get(Expression<Func<TModel, bool>> expression)
        {
            var datosO = DbSet.Where(expression);
            var dat = new List<TEntidad>();

            foreach (var mod in datosO)
            {
                var obj = new TEntidad();
                obj.FromBaseDatos(mod);
                dat.Add(obj);
            }

            return dat;
        }
    }
}
