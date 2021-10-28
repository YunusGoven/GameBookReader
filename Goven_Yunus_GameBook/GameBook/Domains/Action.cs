
namespace GameBook.Domains
{
    public class Action
    {
        /// <summary>
        /// Numero de l action
        /// </summary>
        public int Destination { get; }
        /// <summary>
        /// Texte de laction
        /// </summary>
        public string Content { get; }

        /// <summary>
        /// Constructeur d'une action
        /// </summary>
        /// <param name="destination">numero de l'action</param>
        /// <param name="content">texte de laction</param>
        public Action(string content, int destination)
        {
            this.Destination = destination;
            this.Content = content;
        }

        public string Information => $"{Content} (aller au {Destination})";

        public override bool Equals(object obj)
        {
            if (!(obj is Action))
                return false;
            var action = (Action)obj;
            var numAction = action.Destination;
            return numAction == Destination;

        }

        public override int GetHashCode() => Destination.GetHashCode();

    }
}