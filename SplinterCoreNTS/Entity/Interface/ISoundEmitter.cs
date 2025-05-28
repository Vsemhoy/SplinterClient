using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Interface
{
    public interface ISoundEmitter
    {
        /**
         * Get spl level for frequency
         */
        double GetSPL(int frequency);

        bool HasDirectivity { get; }  // Наличие диаграммы направленности
        DirectivityPattern GetDirectivity(int frequency); // Получение диаграммы направленности
    }
}
