using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Abstracciones.Modelos.Servicios.Peliculas
{
    using System.Collections.Generic;
    using System.Text.Json.Serialization;

    public class TvShowResponse
    {
        public int Page { get; set; }
        public List<TvShow> Results { get; set; }
        public int TotalPages { get; set; }
        public int TotalResults { get; set; }
    }

    public class TvShow
    {
        public bool Adult { get; set; }
        public string BackdropPath { get; set; }
        public List<int> GenreIds { get; set; }
        public int Id { get; set; }
        public List<string> OriginCountry { get; set; }
        public string OriginalLanguage { get; set; }
        public string OriginalName { get; set; }
        public string Overview { get; set; }
        public double Popularity { get; set; }
        [JsonPropertyName("poster_path")]
        public string PosterPath { get; set; }
        [JsonPropertyName("first_air_date")]
        public string FirstAirDate { get; set; }
        public string Name { get; set; }
        [JsonPropertyName("vote_average")]
        public double VoteAverage { get; set; }
        public int VoteCount { get; set; }
    }

}
