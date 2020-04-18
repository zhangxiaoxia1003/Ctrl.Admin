using Ctrl.Core.Business;
using Ctrl.Core.Entities;
using Ctrl.System.DataAccess;
using Ctrl.Domain.Models.Dtos;
using Ctrl.System.Models.Entities;
using System.Threading.Tasks;
using Ctrl.Core.Entities.Paging;
using System.Collections.Generic;
using System;
using Ctrl.Core.Core.Utils;
using Ctrl.Domain.Models.Dtos.Permission;
using Ctrl.Core.Entities.Dtos;
using Ctrl.Core.Core.Resource;

namespace Ctrl.System.Business
{
    /// <summary>
    ///     菜单按钮业务逻辑接口实现
    /// </summary>
    public class SystemMenuButtonLogic:AsyncLogic<SystemMenuButton>,ISystemMenuButtonLogic
    {
        #region 构造函数
        private readonly ISystemMenuButtonRepository _systemMenuButtonRepository;

        public SystemMenuButtonLogic(ISystemMenuButtonRepository systemMenuButtonRepository):base(systemMenuButtonRepository) {
            _systemMenuButtonRepository = systemMenuButtonRepository;
        }

        #endregion

        #region 方法
        /// <summary>
        ///     获取按钮分页
        /// </summary>
        /// <param name="queryParam">分页信息</param>
        /// <returns></returns>
        public Task<PagedResults<SystemMenuButtonOutput>> GetPagingMenuButton(QueryParam param)
        {
            return _systemMenuButtonRepository.GetPagingMenuButton(param);
        }
        /// <summary>
        ///     保存功能项信息  
        /// </summary>
        /// <returns>功能项信息</returns>
        public async Task<OperateStatus> SaveMenuButton(SystemMenuButton menuButton)
        {
            if (menuButton.MenuButtonId.IsEmptyGuid())
            {
                menuButton.CreateTime = DateTime.Now;
                menuButton.MenuButtonId = CombUtil.NewComb();
                return await InsertAsync(menuButton);
            }
            else {
                var  sysbutton=await _systemMenuButtonRepository.GetById(menuButton.MenuButtonId);
                menuButton.CreateTime = sysbutton.CreateTime;
                return await UpdateAsync(menuButton);
            }
        }
        /// <summary>
        ///     根据菜单获取功能项信息
        /// </summary>
        /// <returns></returns>
        public Task<IEnumerable<SystemMenuButtonOutput>> GetMenuButtonByMenuId(IdInput input) {
            return _systemMenuButtonRepository.GetMenuButtonByMenuId(input);
        }

        /// <summary>
        /// 删除
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public async Task<OperateStatus> Delete(IdInput input)
        {
            var OperateStatus = new OperateStatus();
            var Entity = await GetById(input.Id);
            //判断是否存在
            if (Entity == default(SystemMenuButton))
            {
                OperateStatus.ResultSign = ResultSign.Error;
                OperateStatus.Message = Chs.HaveDelete;
                goto Ending;
            }
            OperateStatus = await DeleteAsync(Entity);
        Ending:
            return OperateStatus;
        }
        #endregion
    }
}