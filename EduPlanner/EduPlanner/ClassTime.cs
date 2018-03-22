using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EduPlanner {
    [Serializable]
    public class ClassTime {

        DateTime startTime;
        DateTime endTime;

        public ClassTime(ClassTimeControl classTimeControl) {

            startTime = classTimeControl.tpStartTime.SelectedTime.Value;
            endTime = classTimeControl.tpEndTime.SelectedTime.Value;
        }
    }
}
