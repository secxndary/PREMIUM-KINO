using DevExpress.Mvvm;
using Microsoft.Toolkit.Mvvm.Input;

namespace PREMIUM_KINO
{
    public class ClosingViewModel : ViewModelBase
    {
        public RelayCommand escapeAuthCommand => new(() =>
        {
            Authorization.closeWindow();
        });

        public RelayCommand escapeReserveFormCommand => new(() =>
        {
            ReserveTicketForm.closeWindow();
        });

        public RelayCommand escapeMainWindowCommand => new(() =>
        {
            MainWindowUser.closeWindow();
        });

        public RelayCommand escapeRatingCommand => new(() =>
        {
            SetRating.closeWindow();
        });

        public RelayCommand escapeAddCommand => new(() =>
        {
            AddingFilm.closeWindow();
        });

        public RelayCommand escapeRedactingCommand => new(() =>
        {
            AddingFilm.closeWindow();
        });
    }
}
