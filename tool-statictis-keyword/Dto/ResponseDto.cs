using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace tool_statictis_keyword.Dto
{
    public class ResponseDto<TData>
    {
        public ActionState ActionState { get; set; }
        public string Message { get; set; }
        public TData Data { get; set; }

        public ResponseDto(ActionState state = ActionState.Success, string message = null)
        {
            ActionState = state;
            Message = message;
        }
    }

    public enum ActionState
    {
        Success,
        Warning,
        Error
    }
}
