using NUnit.Framework;
using Shouldly;
using ST.Docs.Samples;

namespace StoryTeller.Testing.ST.Docs.Samples
{
    [TestFixture]
    public class RazorSnippetScannerTester
    {
        [Test]
        public void is_at_start_positive()
        {
            var scanner = new RazorScanner();

            scanner.DetermineName("    @*SAMPLE: UsingCodeSnippetInSpark*@").ShouldBe("UsingCodeSnippetInSpark");
            scanner.DetermineName("@*SAMPLE: UsingCodeSnippetInSpark*@").ShouldBe("UsingCodeSnippetInSpark");
            scanner.DetermineName("@*  SAMPLE:UsingCodeSnippetInSpark  *@").ShouldBe("UsingCodeSnippetInSpark");
            scanner.DetermineName("@*  SAMPLE: UsingCodeSnippetInSpark  *@").ShouldBe("UsingCodeSnippetInSpark");
        }

        [Test]
        public void is_at_start_miss()
        {
            var scanner = new RazorScanner();

            scanner.DetermineName("<h1>some html</h1>").ShouldBeNull();
            scanner.DetermineName("SAMPLE: UsingCodeSnippetInSpark").ShouldBeNull();
        }

        [Test]
        public void is_at_end()
        {
            var scanner = new RazorScanner();

            scanner.IsAtEnd("@* SAMPLE: something").ShouldBeFalse();
            scanner.IsAtEnd("<p>some html</p>").ShouldBeFalse();
            scanner.IsAtEnd("ENDSAMPLE").ShouldBeFalse();
            scanner.IsAtEnd("@* ENDSAMPLE *@").ShouldBeTrue();
        }
    }
}