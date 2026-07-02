// MIT License - Copyright (c) 2025 Marcus Ackre Medina
// See LICENSE file in the project root for full license information.

namespace MarcusMedina.Fluent.Text.Core.Tests.Extensions;
#pragma warning disable IDE0058 // Expression value is never used

using MarcusMedina.Fluent.Text.Core.Extensions.WordSet;
using FluentAssertions;
using Xunit;

public class StringWordSetExtensionsTests
{
    #region JaccardSimilarity Tests

    [Fact]
    public void JaccardSimilarity_IdenticalSets_Returns1()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable" };
        var other = new[] { "Curious", "Adaptable" };

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert
        result.Should().Be(1.0);
    }

    [Fact]
    public void JaccardSimilarity_NoOverlap_Returns0()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable" };
        var other = new[] { "Disciplined", "Patient" };

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert
        result.Should().Be(0.0);
    }

    [Fact]
    public void JaccardSimilarity_PartialOverlap_ReturnsIntersectionOverUnion()
    {
        // Arrange — intersection {Adaptable}, union {Curious, Adaptable, Confident} => 1/3
        var value = new[] { "Curious", "Adaptable" };
        var other = new[] { "Adaptable", "Confident" };

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert
        result.Should().BeApproximately(1.0 / 3.0, 0.0001);
    }

    [Fact]
    public void JaccardSimilarity_BothEmpty_Returns1()
    {
        // Arrange
        var value = Array.Empty<string>();
        var other = Array.Empty<string>();

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert — identical (empty) sets, not "nothing in common"
        result.Should().Be(1.0);
    }

    [Fact]
    public void JaccardSimilarity_OneEmpty_Returns0()
    {
        // Arrange
        var value = new[] { "Curious" };
        var other = Array.Empty<string>();

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert
        result.Should().Be(0.0);
    }

    [Fact]
    public void JaccardSimilarity_DuplicatesWithinASet_TreatedAsSameElement()
    {
        // Arrange — duplicates collapse to a set before comparing
        var value = new[] { "Curious", "Curious", "Adaptable" };
        var other = new[] { "Curious" };

        // Act
        var result = value.JaccardSimilarity(other);

        // Assert — intersection {Curious}, union {Curious, Adaptable} => 1/2
        result.Should().Be(0.5);
    }

    [Fact]
    public void JaccardSimilarity_NullValue_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> value = null!;

        // Act
        var act = () => value.JaccardSimilarity(["Curious"]);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void JaccardSimilarity_NullOther_ThrowsArgumentNullException()
    {
        // Arrange
        var value = new[] { "Curious" };

        // Act
        var act = () => value.JaccardSimilarity(null!);

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion JaccardSimilarity Tests

    #region FrequencyScores Tests

    [Fact]
    public void FrequencyScores_MixedFrequencies_OrdersByCountDescending()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable", "Curious", "Curious", "Adaptable" };

        // Act
        var result = value.FrequencyScores().ToList();

        // Assert
        result.Should().Equal(("Curious", 3), ("Adaptable", 2));
    }

    [Fact]
    public void FrequencyScores_AllUnique_KeepsFirstSeenOrder()
    {
        // Arrange — every item has count 1, so first-seen order should be preserved as the tiebreak
        var value = new[] { "Confident", "Curious", "Adaptable" };

        // Act
        var result = value.FrequencyScores().ToList();

        // Assert
        result.Should().Equal(("Confident", 1), ("Curious", 1), ("Adaptable", 1));
    }

    [Fact]
    public void FrequencyScores_EmptyCollection_ReturnsEmpty()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        var result = value.FrequencyScores();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void FrequencyScores_NullValue_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> value = null!;

        // Act
        var act = () => value.FrequencyScores().ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion FrequencyScores Tests

    #region MostFrequentFirst Tests

    [Fact]
    public void MostFrequentFirst_MixedFrequencies_ReturnsDistinctValuesRankedByCount()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable", "Curious", "Curious", "Adaptable" };

        // Act
        var result = value.MostFrequentFirst().ToList();

        // Assert
        result.Should().Equal("Curious", "Adaptable");
    }

    [Fact]
    public void MostFrequentFirst_EmptyCollection_ReturnsEmpty()
    {
        // Arrange
        var value = Array.Empty<string>();

        // Act
        var result = value.MostFrequentFirst();

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void MostFrequentFirst_NullValue_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> value = null!;

        // Act
        var act = () => value.MostFrequentFirst().ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion MostFrequentFirst Tests

    #region PresentInAll Tests

    [Fact]
    public void PresentInAll_ValueInEveryCollection_IsReturned()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable", "Disciplined" };
        var other1 = new[] { "Adaptable", "Confident" };
        var other2 = new[] { "Adaptable", "Patient" };

        // Act
        var result = value.PresentInAll(other1, other2).ToList();

        // Assert
        result.Should().Equal("Adaptable");
    }

    [Fact]
    public void PresentInAll_NoOthersGiven_ReturnsDistinctValueItems()
    {
        // Arrange
        var value = new[] { "Curious", "Curious", "Adaptable" };

        // Act
        var result = value.PresentInAll().ToList();

        // Assert
        result.Should().Equal("Curious", "Adaptable");
    }

    [Fact]
    public void PresentInAll_NoOverlapAcrossAll_ReturnsEmpty()
    {
        // Arrange
        var value = new[] { "Curious" };
        var other = new[] { "Adaptable" };

        // Act
        var result = value.PresentInAll(other);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void PresentInAll_NullValue_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> value = null!;

        // Act
        var act = () => value.PresentInAll(["Curious"]).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void PresentInAll_NullOthers_ThrowsArgumentNullException()
    {
        // Arrange
        var value = new[] { "Curious" };

        // Act
        var act = () => value.PresentInAll(null!).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion PresentInAll Tests

    #region UniqueTo Tests

    [Fact]
    public void UniqueTo_ValueNotInOthers_IsReturned()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable", "Disciplined" };
        var other1 = new[] { "Adaptable" };
        var other2 = new[] { "Disciplined" };

        // Act
        var result = value.UniqueTo(other1, other2).ToList();

        // Assert
        result.Should().Equal("Curious");
    }

    [Fact]
    public void UniqueTo_NoOthersGiven_ReturnsDistinctValueItems()
    {
        // Arrange
        var value = new[] { "Curious", "Curious", "Adaptable" };

        // Act
        var result = value.UniqueTo().ToList();

        // Assert
        result.Should().Equal("Curious", "Adaptable");
    }

    [Fact]
    public void UniqueTo_EverythingOverlaps_ReturnsEmpty()
    {
        // Arrange
        var value = new[] { "Curious", "Adaptable" };
        var other = new[] { "Curious", "Adaptable" };

        // Act
        var result = value.UniqueTo(other);

        // Assert
        result.Should().BeEmpty();
    }

    [Fact]
    public void UniqueTo_NullValue_ThrowsArgumentNullException()
    {
        // Arrange
        IEnumerable<string> value = null!;

        // Act
        var act = () => value.UniqueTo(["Curious"]).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    [Fact]
    public void UniqueTo_NullOthers_ThrowsArgumentNullException()
    {
        // Arrange
        var value = new[] { "Curious" };

        // Act
        var act = () => value.UniqueTo(null!).ToList();

        // Assert
        act.Should().Throw<ArgumentNullException>();
    }

    #endregion UniqueTo Tests
}
