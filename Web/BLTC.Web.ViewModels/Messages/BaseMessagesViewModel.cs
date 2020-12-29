namespace BLTC.Web.ViewModels.Messages
{
    public abstract class BaseMessagesViewModel : BaseContactMessageInputModel
    {
        private string id;

        public string Id
        {
            get { return this.id; }
            set { this.SetProperty(ref this.id, value); }
        }
    }
}
