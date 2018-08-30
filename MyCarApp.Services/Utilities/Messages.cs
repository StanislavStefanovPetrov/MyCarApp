namespace MyCarApp.Services.Utilities
{
    public static class Messages
    {
        public const string MakeAlreadyExists = "A make with name: {0} already exists!";

        public const string MakeWithIdNoExists = "A make with id: {0} does not exists!";

        public const string ModelWithIdNoExists = "A model with id: {0} does not exists!";

        public const string ModelNameDublicatesExists = "You are not allowed to add models with same names!";

        public const string ModelAlreadyExists = "A model with name: {0} already exists in make: {1} production models!";

        public const string EntityDoesNoExists = "Invalid {0} type. Please try again!";

        public const string MakeModelDiscrepancy = "This make: {0} has no such model: {1}!";

        public const string ForbiddenFileExtention = "Picture with extention: {0} is now approved to be uploaded!";

        public const string BigPictureSize = "This picture too big. Please choose another!";

        public const string AdvDoesNoExists = "Advertisement does not exists!";

        public const string AdvDenyAccess = "You have no access to this advertisement!";

        public const string AdvAlreadyAddedToUser = "You have been added the advertisement to your faverite already. You can not add it again!";

        public const string AdvSellerCannotAdd = "You have published that advertisement. You can find it into you \"Published Advertisements\" list!";

        public const string AdvUserIsNotSeller = "You have no permission to manipulate that advertisement!";

        public const string AdvIsExpiredAlready = "This advertisement is expired already!";

        public const string InvalidEntityProperties = "The search binding model is not valid!";

        public const string PasswordSuccessfullyChanged = "Password have been successfully changed.";

        public const string UserSuccessfullyBanned = "The user have been successfully banned.";

        public const string UserSuccessfullyUnBanned = "The user have been successfully unbanned.";
    }
}
