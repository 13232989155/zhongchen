using Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace zhongchen.Models
{
    public class SearchResult
    {

        public int typeId { set; get; }

        public string searchString { set; get; }

        public List<VideoEntity> videoEntities { set; get; }

        public List<RecipeEntity> recipeEntities { set; get; }


        public List<ProductEntity> productEntities { set; get;  }
    }
}
