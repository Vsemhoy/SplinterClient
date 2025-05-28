using SplinterCoreNTS.Entity.Props;
using System;
using System.Collections.Generic;
using System.Text;

namespace SplinterCoreNTS.Entity.Interface
{
    /**
     * Base interface for all sound emitters
     */
    public interface ISoundEmitter
    {
        /**
         * Get spl level for frequency
         */
        double GetSPL(int frequency);
        /**
         * Is this emitter type has direction pattern
         */
        bool HasDirectivity { get; }  // Наличие диаграммы направленности
        DirectivityPattern GetDirectivity(int frequency); // Получение диаграммы направленности
    }


    public interface IDirectivityEmitter : ISoundEmitter
    {
        DirectivityPattern GetDirectivity(int frequency);
    }
}
