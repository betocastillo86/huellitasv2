//-----------------------------------------------------------------------
// <copyright file="ChangeContentStatusTask.cs" company="Huellitas sin hogar">
//     Company copyright tag.
// </copyright>
//-----------------------------------------------------------------------
namespace Huellitas.Business.Tasks
{
    using System.Threading.Tasks;
    using Huellitas.Business.Services;

    /// <summary>
    /// Change Content Status Task
    /// </summary>
    /// <seealso cref="Huellitas.Business.Tasks.ITask" />
    public class ChangeContentStatusTask : ITask
    {
        /// <summary>
        /// The content service
        /// </summary>
        private readonly IContentService contentService;

        /// <summary>
        /// Initializes a new instance of the <see cref="ChangeContentStatusTask"/> class.
        /// </summary>
        /// <param name="contentService">The content service.</param>
        public ChangeContentStatusTask(
            IContentService contentService)
        {
            this.contentService = contentService;
        }

        /// <summary>
        /// Disables the pet after days.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the task</returns>
        public async Task DisablePetAfterDays(int id)
        {
            var content = this.contentService.GetById(id);
            content.StatusType = Data.Entities.StatusType.Hidden;
            await this.contentService.UpdateAsync(content);
        }
    }
}