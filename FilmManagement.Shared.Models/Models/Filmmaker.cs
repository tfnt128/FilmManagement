namespace FilmManagement.Shared.Models.Models
{
    public class Filmmaker
    {
        public virtual ICollection<Films> Films { get; set; } = new List<Films>();

        public Filmmaker()
        {
            PictureProfile = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        }
        public Filmmaker(string name, string bio)
        {
            Name = name;
            Bio = bio;
            PictureProfile = "https://cdn.pixabay.com/photo/2016/08/08/09/17/avatar-1577909_1280.png";
        }

        public string Name { get; set; }
        public string PictureProfile { get; set; }
        public string Bio { get; set; }
        public int Id { get; set; }
    }
}
