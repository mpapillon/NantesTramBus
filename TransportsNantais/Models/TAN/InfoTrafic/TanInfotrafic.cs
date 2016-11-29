using Newtonsoft.Json;

namespace TransportsNantais.Models.TAN.InfoTrafic
{

    public class Rootobject
    {
        public Opendata opendata { get; set; }
    }

    public class Opendata
    {
        public string request { get; set; }
        public Answer answer { get; set; }
    }

    public class Answer
    {
        public Status status { get; set; }
        public Data data { get; set; }
    }

    public class Status
    {
        public Attributes attributes { get; set; }
    }

    public class Attributes
    {
        public string code { get; set; }
        public string message { get; set; }
    }

    public class Data
    {
        public Root ROOT { get; set; }
    }

    [JsonObject("ROOT")]
    public class Root
    {
        public ListInfortrafics LISTE_INFOTRAFICS { get; set; }
    }

    [JsonObject("LISTE_INFOTRAFICS")]
    public class ListInfortrafics
    {
        [JsonProperty("INFOTRAFIC")]
        public InfoTrafic[] InfoTrafics { get; set; }
    }

    [JsonObject("INFOTRAFIC")]
    public class InfoTrafic
    {
        [JsonProperty("CODE")]
        public string Code { get; set; }

        [JsonProperty("LANGUE")]
        public string Langue { get; set; }

        [JsonProperty("INTITULE")]
        public string Intitule { get; set; }

        [JsonProperty("RESUME")]
        public string Resume { get; set; }

        [JsonProperty("TEXTE_VOCAL")]
        public string TexteVocal { get; set; }

        [JsonProperty("DATE_DEBUT")]
        public string DateDebut { get; set; }

        [JsonProperty("DATE_FIN")]
        public string DateFin { get; set; }

        [JsonProperty("HEURE_DEBUT")]
        public string HeureDebut { get; set; }

        [JsonProperty("HEURE_FIN")]
        public string HeureFin { get; set; }

        [JsonProperty("PERTURBATION_TERMINEE")]
        public string PerturbationTerminee { get; set; }

        [JsonProperty("TRONCONS")]
        public string Troncons { get; set; }
    }

}
