using System;
using System.Collections.Generic;

namespace DoerLib
{
    /// <summary>
    /// Used to execute actions, based on given conditions.
    /// </summary>
    /// <typeparam name="T">Type of element that is being used for this doer</typeparam>
    public class Doer<T>
    {
        /// <summary>
        /// Defined rules for actions on conditions
        /// </summary>
        private List<ConditionAction<T>> conditionActions = new List<ConditionAction<T>>();

        /// <summary>
        /// Action for when the given element matches no other defined rules
        /// </summary>
        private Action<T> wildcard;

        /// <summary>
        /// Adds a new rule for the doer
        /// </summary>
        /// <param name="condition">Condition that has to be met for the action to execute</param>
        /// <param name="action">Action, that is executed, if the condition is met</param>
        /// <returns></returns>
        public Doer<T> On(Predicate<T> condition, Action<T> action)
        {
            conditionActions.Add(new ConditionAction<T>(condition, action));
            return this;
        }

        /// <summary>
        /// Adds an action, which is executed if the element matchs no other defined rules
        /// </summary>
        /// <param name="action"></param>
        /// <returns></returns>
        public Doer<T> OnElse(Action<T> action)
        {
            wildcard = action;
            return this;
        }

        public void Do(IEnumerable<T> items)
        {
            // Iterate through all the items
            foreach (T item in items)
            {
                bool found = false;

                // Check all the conditions
                foreach (ConditionAction<T> conAc in conditionActions)
                {
                    // If item meets a condition, execute the action for that condition
                    if (conAc.Condition(item))
                    {
                        conAc.Action(item);
                        found = true;
                    }
                }

                // If we found no other rules, lets check if we have a wildcard
                if (!found && wildcard != null)
                {
                    wildcard(item);
                }
            }
        }

        private class ConditionAction<Tn>
        {
            /// <summary>
            /// Condition, which has to be met for the action to be executed
            /// </summary>
            public Predicate<Tn> Condition { get; private set; }

            /// <summary>
            /// Action which is executed if the condition is met
            /// </summary>
            public Action<Tn> Action { get; private set; }

            public ConditionAction(Predicate<Tn> condition, Action<Tn> action)
            {
                Condition = condition;
                Action = action;
            }
        }
    }
}
