namespace Application;

public class MimeTypeTests
{
    [Theory]
    [InlineData("", null)]
    [InlineData(null, null)]
    [InlineData("no file extension", null)]
    [InlineData("file.csv", "text/csv")]
    [InlineData("document.docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document")]
    [InlineData("image.jpg", "image/jpeg")]
    [InlineData("presentation.pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation")]
    [InlineData("archive.zip", "application/zip")]
    public void ToMimeType_ReturnsExpectedResult(string filename, string expectedMimeType)
    {
        // arrange & act
        var mimeType = filename.ToMimeType();

        // assert
        Assert.Equal(expectedMimeType, mimeType);
    }

    [Fact]
    public void ToMimeType_UnknownExtension_ThrowsKeyNotFoundException()
    {
        // assert
        Assert.Throws<KeyNotFoundException>(() => "unknown.xyz".ToMimeType());
    }
}