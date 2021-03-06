﻿using System.Windows.Automation;

namespace Automation.UI.Tree {

    /// <summary>
    ///     A special property condition that can partially match string properties.
    ///     Matching is done using string functions like StartsWith, EndsWith and Contains.
    ///     This also supports matching properties using regular expressions.
    /// </summary>
    internal class StringPropertyCondition : PropertyCondition {

        /// <summary>
        ///     New string property condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected string value.</param>
        public StringPropertyCondition(AutomationProperty property, string value)
            : this(property, value, Util.Matchers.Contains) {}

        /// <summary>
        ///     New string property condition.
        /// </summary>
        /// <param name="property">The property to check.</param>
        /// <param name="value">The expected string value.</param>
        /// <param name="matcher">The matcher to use for the comparison.</param>
        public StringPropertyCondition(AutomationProperty property, string value, Matcher<string> matcher)
            : base(property, value) {
            Matcher = matcher;
        }

        /// <summary>
        ///     Get the matcher used by this condition.
        /// </summary>
        public Matcher<string> Matcher { get; private set; }

        /// <summary>
        ///     Check if the specified automation element meets this condition's requirements.
        /// </summary>
        /// <param name="element">The element to check.</param>
        /// <returns>True if the automation element meets this condition's requirements.</returns>
        public bool IsMatch(AutomationElement element) {
            var actualObj = element.GetCurrentPropertyValue(Property);
            // If the element's current property value is null or not a string object, return false.
            if (actualObj == null || !(actualObj is string))
                return false;
            // Otherwise cast to strings...
            var actual = (string) actualObj;
            var expected = (string) Value;
            // ... and compare them.
            return Matcher.IsMatch(actual, expected);
        }

    }

}
