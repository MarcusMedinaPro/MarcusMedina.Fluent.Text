// MIT License - Copyright (c) 2025 Marcus Ackre Medina
// See LICENSE file in the project root for full license information.

namespace MarcusMedina.Fluent.Text.Core.Extensions.WordSet;

/// <summary>
/// Extension methods for comparing and ranking collections of words or tags — Jaccard similarity,
/// frequency ranking, and set-style shortcuts over multiple collections.
/// </summary>
public static class StringWordSetExtensions
{
    #region Public Methods

    /// <summary>
    /// Calculates the Jaccard similarity between two word sets — the size of their intersection
    /// divided by the size of their union. Two empty sets are considered identical (1.0); one
    /// empty and one non-empty set share nothing (0.0).
    /// </summary>
    /// <param name="value">The first word set.</param>
    /// <param name="other">The word set to compare against.</param>
    /// <returns>A similarity score between 0.0 (nothing in common) and 1.0 (identical sets).</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> or <paramref name="other"/> is null.</exception>
    /// <example>
    /// <code>
    /// new[] { "Curious", "Adaptable" }.JaccardSimilarity(new[] { "Adaptable", "Confident" })  // 1/3
    /// </code>
    /// </example>
    public static double JaccardSimilarity(this IEnumerable<string> value, IEnumerable<string> other)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(other);

        var setA = value.ToHashSet();
        var setB = other.ToHashSet();

        if (setA.Count == 0 && setB.Count == 0)
        {
            return 1.0;
        }

        var intersectionCount = setA.Intersect(setB).Count();
        var unionCount = setA.Union(setB).Count();

        return unionCount == 0 ? 0.0 : (double)intersectionCount / unionCount;
    }

    /// <summary>
    /// Sorts a collection so the most frequently occurring values come first. Duplicate values in
    /// the input contribute to the same item's rank rather than appearing as separate entries.
    /// </summary>
    /// <param name="value">The words to rank by frequency.</param>
    /// <returns>The distinct values, ordered from most to least frequent. Ties keep first-seen order.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    /// <example>
    /// <code>
    /// new[] { "Curious", "Adaptable", "Curious" }.MostFrequentFirst()  // ["Curious", "Adaptable"]
    /// </code>
    /// </example>
    public static IEnumerable<string> MostFrequentFirst(this IEnumerable<string> value)
    {
        ArgumentNullException.ThrowIfNull(value);

        return value
            .FrequencyScores()
            .Select(entry => entry.Item);
    }

    /// <summary>
    /// Counts how many times each distinct value occurs in the collection.
    /// </summary>
    /// <param name="value">The words to count.</param>
    /// <returns>Each distinct item paired with its occurrence count, ordered from most to least frequent. Ties keep first-seen order.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> is null.</exception>
    /// <example>
    /// <code>
    /// new[] { "Curious", "Adaptable", "Curious" }.FrequencyScores()  // [("Curious", 2), ("Adaptable", 1)]
    /// </code>
    /// </example>
    public static IEnumerable<(string Item, int Count)> FrequencyScores(this IEnumerable<string> value)
    {
        ArgumentNullException.ThrowIfNull(value);

        var materialized = value.ToList();
        var firstSeenOrder = materialized.Distinct().ToList();

        return firstSeenOrder
            .Select(item => (Item: item, Count: materialized.Count(w => w == item)))
            .OrderByDescending(entry => entry.Count)
            .ThenBy(entry => firstSeenOrder.IndexOf(entry.Item));
    }

    /// <summary>
    /// Finds the values that are present in this collection and every one of the other collections —
    /// a named shortcut for chaining <see cref="Enumerable.Intersect{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>.
    /// </summary>
    /// <param name="value">The base collection.</param>
    /// <param name="others">The other collections a value must also appear in.</param>
    /// <returns>The values common to <paramref name="value"/> and every collection in <paramref name="others"/>. Returns <paramref name="value"/>'s distinct items unchanged if <paramref name="others"/> is empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> or <paramref name="others"/> is null.</exception>
    /// <example>
    /// <code>
    /// new[] { "Curious", "Adaptable" }.PresentInAll(new[] { "Adaptable", "Confident" })  // ["Adaptable"]
    /// </code>
    /// </example>
    public static IEnumerable<string> PresentInAll(this IEnumerable<string> value, params IEnumerable<string>[] others)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(others);

        return others.Aggregate(value.Distinct(), (current, other) => current.Intersect(other));
    }

    /// <summary>
    /// Finds the values that are present in this collection but in none of the other collections —
    /// a named shortcut for chaining <see cref="Enumerable.Except{TSource}(IEnumerable{TSource}, IEnumerable{TSource})"/>.
    /// </summary>
    /// <param name="value">The base collection.</param>
    /// <param name="others">The other collections a value must not appear in.</param>
    /// <returns>The values found only in <paramref name="value"/>. Returns <paramref name="value"/>'s distinct items unchanged if <paramref name="others"/> is empty.</returns>
    /// <exception cref="ArgumentNullException">Thrown when <paramref name="value"/> or <paramref name="others"/> is null.</exception>
    /// <example>
    /// <code>
    /// new[] { "Curious", "Adaptable" }.UniqueTo(new[] { "Adaptable", "Confident" })  // ["Curious"]
    /// </code>
    /// </example>
    public static IEnumerable<string> UniqueTo(this IEnumerable<string> value, params IEnumerable<string>[] others)
    {
        ArgumentNullException.ThrowIfNull(value);
        ArgumentNullException.ThrowIfNull(others);

        return others.Aggregate(value.Distinct(), (current, other) => current.Except(other));
    }

    #endregion Public Methods
}
