using System.Threading.Tasks;

namespace FSMExpress.Services;
public interface IDialogService
{
    Task ShowDialog(IDialogAware viewModel);

    Task<TResult?> ShowDialog<TResult>(IDialogAware<TResult> viewModel);
}
