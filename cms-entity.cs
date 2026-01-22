public class Content
{
    /// <summary>
    /// Unique identifier for the content
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Title of the content
    /// </summary>
    public string Title { get; private set; } = string.Empty;

    /// <summary>
    /// Brief summary of the content
    /// </summary>
    public string Summary { get; private set; } = string.Empty;

    /// <summary>
    /// URL-friendly slug for SEO and readable URLs
    /// </summary>
    public string Slug { get; private set; } = string.Empty;

    /// <summary>
    /// Current status of the content (Draft, Published, Archived)
    /// </summary>
    public ContentStatus Status { get; private set; }

    /// <summary>
    /// Timestamp when the content was created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Timestamp of the last update to the content
    /// </summary>
    public DateTime LastUpdatedAt { get; private set; }

    /// <summary>
    /// Timestamp when the content was published
    /// </summary>
    public DateTime? PublishedAt { get; private set; }

    /// <summary>
    /// URL or path to the cover image
    /// </summary>
    public string? CoverImageUrl { get; private set; }

    /// <summary>
    /// Language code (e.g., "ko", "en", "ja")
    /// </summary>
    public string? Lang { get; private set; }

    /// <summary>
    /// Estimated time to read this content in minutes
    /// </summary>
    public int? TimeToRead { get; private set; }

    /// <summary>
    /// Main content body stored as JSON document (EditorJS format)
    /// </summary>
    public JsonDocument BodyContent { get; private set; } = JsonDocument.Parse("{}");

    /// <summary>
    /// Foreign key to the space (Blog, Tech Docs, etc.)
    /// </summary>
    public int SpaceId { get; private set; }

    /// <summary>
    /// Number of times this content has been viewed
    /// </summary>
    public int ViewCount { get; private set; }

    /// <summary>
    /// Concurrency token for optimistic concurrency control (낙관적 동시성 제어)
    /// </summary>
    public byte[]? RowVersion { get; private set; }

    /// <summary>
    /// Navigation property to the space
    /// </summary>
    public virtual Space? Space { get; private set; }

    // Private backing fields for encapsulation
    private readonly List<ContentImage> _contentImages = new();
    private readonly List<ContentAttachment> _contentAttachments = new();
    private readonly List<ContentTag> _contentTags = new();
    private readonly List<ContentTopic> _contentTopics = new();
    private readonly List<ContentAuthor> _contentAuthors = new();

    // EF Core가 사용할 수 있도록 노출 (읽기 전용)
    public IReadOnlyCollection<ContentAuthor> ContentAuthors => _contentAuthors.AsReadOnly();
    public IReadOnlyCollection<ContentTag> ContentTags => _contentTags.AsReadOnly();
    public IReadOnlyCollection<ContentTopic> ContentTopics => _contentTopics.AsReadOnly();
    public IReadOnlyCollection<ContentImage> ContentImages => _contentImages.AsReadOnly();
    public IReadOnlyCollection<ContentAttachment> ContentAttachments => _contentAttachments.AsReadOnly();

    /// <summary>
    /// Authors associated with this content (creators and editors)
    /// </summary>
    public IEnumerable<AppUser> Authors => _contentAuthors.Select(ca => ca.AppUser);

    /// <summary>
    /// Images associated with this content
    /// </summary>
    public IEnumerable<Image> Images => _contentImages.Select(ci => ci.Image);

    /// <summary>
    /// Attachments associated with this content
    /// </summary>
    public IEnumerable<Attachment> Attachments => _contentAttachments.Select(ca => ca.Attachment);

    /// <summary>
    /// Tags associated with this content
    /// </summary>
    public IEnumerable<Tag> Tags => _contentTags.Select(ct => ct.Tag);

    /// <summary>
    /// Topics associated with this content
    /// </summary>
    public IEnumerable<Topic> Topics => _contentTopics.Select(ct => ct.Topic);
}

/// <summary>
/// Junction entity for the many-to-many relationship between Content and Attachment
/// </summary>
public class ContentAttachment
{
    /// <summary>
    /// Foreign key to the content
    /// </summary>
    public long ContentId { get; set; }

    /// <summary>
    /// Foreign key to the attachment
    /// </summary>
    public long AttachmentId { get; set; }

    /// <summary>
    /// Optional display order of the attachment within the content
    /// </summary>
    public int? OrderIndex { get; set; }

    /// <summary>
    /// Timestamp when the attachment was attached to the content
    /// </summary>
    public DateTime AttachedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property to the content
    /// </summary>
    public virtual Content Content { get; set; } = null!;

    /// <summary>
    /// Navigation property to the attachment
    /// </summary>
    public virtual Attachment Attachment { get; set; } = null!;
}

/// <summary>
/// Junction entity for the many-to-many relationship between Content and AppUser (as content author)
/// Tracks who can edit a specific piece of content
/// </summary>
public class ContentAuthor
{
    /// <summary>
    /// Foreign key to the content
    /// </summary>
    public long ContentId { get; set; }

    /// <summary>
    /// Foreign key to the author (AppUser)
    /// </summary>
    public Guid AuthorId { get; set; }

    /// <summary>
    /// When this author was added to the content
    /// </summary>
    public DateTime AddedAt { get; set; }

    /// <summary>
    /// Whether this is the original creator (true for the first author)
    /// Only the creator can invite other authors
    /// </summary>
    public bool IsCreator { get; set; }

    /// <summary>
    /// Navigation property to the content
    /// </summary>
    public virtual Content Content { get; set; } = null!;

    /// <summary>
    /// Navigation property to the app user
    /// </summary>
    public virtual AppUser AppUser { get; set; } = null!;
}

/// <summary>
/// Junction entity for the many-to-many relationship between Content and Image
/// </summary>
public class ContentImage
{
    /// <summary>
    /// Foreign key to the content
    /// </summary>
    public long ContentId { get; set; }

    /// <summary>
    /// Foreign key to the image
    /// </summary>
    public long ImageId { get; set; }

    /// <summary>
    /// Timestamp when the image was attached to the content
    /// </summary>
    public DateTime AttachedAt { get; set; } = DateTime.UtcNow;

    /// <summary>
    /// Navigation property to the content
    /// </summary>
    public virtual Content Content { get; set; } = null!;

    /// <summary>
    /// Navigation property to the image
    /// </summary>
    public virtual Image Image { get; set; } = null!;
}

/// <summary>
/// Content 정렬 기준
/// </summary>
public enum ContentSortBy
{
    /// <summary>
    /// 발행일 또는 생성일 기준 (기본값)
    /// </summary>
    PublishedAt = 0,

    /// <summary>
    /// 생성일 기준
    /// </summary>
    CreatedAt = 1,

    /// <summary>
    /// 조회수 기준
    /// </summary>
    ViewCount = 2,

    /// <summary>
    /// 제목 기준 (가나다순)
    /// </summary>
    Title = 3,

    /// <summary>
    /// 최종 수정일 기준
    /// </summary>
    LastUpdatedAt = 4
}

/// <summary>
/// Defines the publication status of content
/// </summary>
public enum ContentStatus
{
    /// <summary>
    /// Temporal saves by system automatically
    /// </summary>
    Temporary = 0,

    /// <summary>
    /// Content is in draft state and not publicly visible
    /// </summary>
    Draft = 1,

    /// <summary>
    /// Content is published and publicly accessible
    /// </summary>
    Published = 2,

    /// <summary>
    /// Content has been archived and is no longer actively displayed
    /// </summary>
    Archived = 999
}

/// <summary>
/// Junction entity for the many-to-many relationship between Content and Tag
/// </summary>
public class ContentTag
{
    /// <summary>
    /// Foreign key to the content
    /// </summary>
    public long ContentId { get; set; }

    /// <summary>
    /// Foreign key to the tag
    /// </summary>
    public long TagId { get; set; }

    /// <summary>
    /// Navigation property to the content
    /// </summary>
    public virtual Content Content { get; set; } = null!;

    /// <summary>
    /// Navigation property to the tag
    /// </summary>
    public virtual Tag Tag { get; set; } = null!;
}

/// <summary>
/// Junction entity for the many-to-many relationship between Content and Topic
/// </summary>
public class ContentTopic
{
    /// <summary>
    /// Foreign key to the content
    /// </summary>
    public long ContentId { get; set; }

    /// <summary>
    /// Foreign key to the topic
    /// </summary>
    public long TopicId { get; set; }

    /// <summary>
    /// Navigation property to the content
    /// </summary>
    public virtual Content Content { get; set; } = null!;

    /// <summary>
    /// Navigation property to the topic
    /// </summary>
    public virtual Topic Topic { get; set; } = null!;
}

public class Attachment
{
    /// <summary>
    /// EF Core constructor
    /// </summary>
    private Attachment()
    {
        // Required for EF Core
    }

    /// <summary>
    /// Domain constructor for creating a new attachment
    /// </summary>
    public Attachment(
        string originalFilename,
        string path,
        string storageFilename,
        long size,
        string? mimeType = null,
        Guid? uploadedBy = null)
    {
        OriginalFilename = originalFilename;
        Path = path;
        StorageFilename = storageFilename;
        Size = size;
        MimeType = mimeType;
        UploadedBy = uploadedBy;
        UploadedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Unique identifier for the attachment
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Original filename as uploaded by the user
    /// </summary>
    public string OriginalFilename { get; private set; } = string.Empty;

    /// <summary>
    /// Relative storage path (e.g., "2025/01/file-guid.pdf")
    /// </summary>
    public string Path { get; private set; } = string.Empty;

    /// <summary>
    /// GUID-based filename used in storage
    /// </summary>
    public string StorageFilename { get; private set; } = string.Empty;

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// MIME type of the file (e.g., "application/pdf", "application/vnd.ms-excel")
    /// </summary>
    public string? MimeType { get; private set; }

    /// <summary>
    /// User who uploaded the file (optional tracking)
    /// </summary>
    public Guid? UploadedBy { get; private set; }

    /// <summary>
    /// Timestamp when the file was uploaded
    /// </summary>
    public DateTime UploadedAt { get; private set; }

    // Private backing field for encapsulation
    private readonly List<ContentAttachment> _contentAttachments = new();

    /// <summary>
    /// Contents that use this attachment
    /// </summary>
    public IEnumerable<Content> Contents => _contentAttachments.Select(ca => ca.Content);

    /// <summary>
    /// Gets the download URL for accessing the attachment
    /// </summary>
    public string DownloadUrl => $"/attachment/{Id}/download";

    /// <summary>
    /// Gets the file size formatted as human-readable string (e.g., "1.5 MB")
    /// </summary>
    public string SizeFormatted => FormatBytes(Size);

    /// <summary>
    /// Gets the file extension from the original filename
    /// </summary>
    public string FileExtension => System.IO.Path.GetExtension(OriginalFilename);
}

/// <summary>
/// Represents an image entity in the media library
/// </summary>
public class Image
{
    /// <summary>
    /// EF Core constructor
    /// </summary>
    private Image()
    {
        // Required for EF Core
    }

    /// <summary>
    /// Domain constructor for creating a new image
    /// </summary>
    public Image(
        Guid authorId,
        string originalFilename,
        string path,
        long size,
        string? mimeType = null,
        string? altText = null,
        int? width = null,
        int? height = null)
    {
        AuthorId = authorId;
        OriginalFilename = originalFilename;
        RelativeStoredPath = path;
        Size = size;
        MimeType = mimeType;
        AltText = altText;
        Width = width;
        Height = height;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Unique identifier for the image
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Foreign key to the author who uploaded the image
    /// </summary>
    public Guid AuthorId { get; private set; }

    /// <summary>
    /// Original filename as uploaded by the user
    /// </summary>
    public string OriginalFilename { get; private set; } = string.Empty;

    /// <summary>
    /// Relative storage path (e.g., "images/202511/abc.jpg")  
    /// </summary>
    /// <remarks>
    /// The actual absolute file path is combined with <c>RootPath</c>.
    /// <br />
    /// ���� ���� ��δ� <c>RootPath</c>�� ���յ� ���·� ���˴ϴ�.  
    /// ��: RootPath/images/202511/abc.jpg
    /// </remarks>
    public string RelativeStoredPath { get; private set; } = string.Empty;

    /// <summary>
    /// File size in bytes
    /// </summary>
    public long Size { get; private set; }

    /// <summary>
    /// MIME type of the image (e.g., "image/jpeg", "image/png")
    /// </summary>
    public string? MimeType { get; private set; }

    /// <summary>
    /// Alternative text for accessibility (WCAG compliance)
    /// </summary>
    public string? AltText { get; private set; }

    /// <summary>
    /// Image width in pixels
    /// </summary>
    public int? Width { get; private set; }

    /// <summary>
    /// Image height in pixels
    /// </summary>
    public int? Height { get; private set; }

    /// <summary>
    /// Timestamp when the image was uploaded
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Navigation property to the user who uploaded this image
    /// </summary>
    public virtual AppUser? AppUser { get; private set; }

    // Private backing field for encapsulation
    private readonly List<ContentImage> _contentImages = new();

    /// <summary>
    /// Contents that use this image
    /// </summary>
    public IEnumerable<Content> Contents => _contentImages.Select(ci => ci.Content);
}

/// <summary>
/// Represents a content space (organizational unit for grouping related content)
/// Examples: Blog, Tech Documentation, Internal Wiki, Customer Portal
/// A Space is an independent content area within an organization with its own access and publishing policies
/// </summary>
public class Space
{
    // Private backing fields for encapsulation
    private readonly List<Content> _contents = new();
    private readonly List<Topic> _topics = new();
    private readonly List<Tag> _tags = new();
    private readonly List<ApiKey> _apiKeys = new();
    private readonly List<SpaceMember> _spaceMembers = new();

    /// <summary>
    /// Domain constructor for creating a new space
    /// </summary>
    /// <param name="name">Space name</param>
    /// <param name="description">Space description</param>
    /// <param name="type">Space type (Open/Moderated/Closed)</param>
    /// <param name="creatorId">User ID of the creator (becomes first Administrator)</param>
    public Space(string name, string description, SpaceType type, Guid creatorId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Space name cannot be empty", nameof(name));

        Name = name;
        Description = description ?? string.Empty;
        Type = type;
        CreatorId = creatorId;
        CreatedAt = DateTime.UtcNow;

        // [SPACE-2] Creator becomes Administrator automatically
        _spaceMembers.Add(new SpaceMember
        {
            SpaceId = this.Id,
            UserId = creatorId,
            Role = SpaceRole.Administrator,
            JoinedAt = DateTime.UtcNow
        });
    }

    /// <summary>
    /// Unique identifier for the space
    /// </summary>
    public int Id { get; private set; }

    /// <summary>
    /// Display name of the space (e.g., "Blog", "Tech Docs", "Internal Wiki")
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Description of the space's purpose
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Space type determining access and publishing policy
    /// </summary>
    public SpaceType Type { get; private set; }

    /// <summary>
    /// User ID of the Space creator (permanent Administrator)
    /// </summary>
    public Guid CreatorId { get; private set; }

    /// <summary>
    /// Timestamp when the space was created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Contents that belong to this space (read-only)
    /// Note: This is a navigation property for EF Core, not part of the domain model boundary
    /// </summary>
    public IReadOnlyCollection<Content> Contents => _contents.AsReadOnly();

    /// <summary>
    /// Topics that belong to this space (read-only)
    /// Note: This is a navigation property for EF Core, not part of the domain model boundary
    /// </summary>
    public IReadOnlyCollection<Topic> Topics => _topics.AsReadOnly();

    /// <summary>
    /// Topics that belong to this space (read-only)
    /// Note: This is a navigation property for EF Core, not part of the domain model boundary
    /// </summary>
    public IReadOnlyCollection<Tag> Tags => _tags.AsReadOnly();

    /// <summary>
    /// API keys for external client access to this space (read-only)
    /// Note: This is a navigation property for EF Core
    /// </summary>
    public IReadOnlyCollection<ApiKey> ApiKeys => _apiKeys.AsReadOnly();

    /// <summary>
    /// Space members with their roles (read-only)
    /// </summary>
    public IReadOnlyCollection<SpaceMember> SpaceMembers => _spaceMembers.AsReadOnly();
}
/// <summary>
/// Junction entity for the many-to-many relationship between Space and AppUser (Space membership)
/// Space와 AppUser의 다대다 관계 (Space 멤버십)
/// </summary>
public class SpaceMember
{
    /// <summary>
    /// Foreign key to the Space
    /// </summary>
    public int SpaceId { get; set; }

    /// <summary>
    /// Foreign key to the user
    /// </summary>
    public Guid UserId { get; set; }

    /// <summary>
    /// Role of this user in the Space
    /// </summary>
    public SpaceRole Role { get; set; }

    /// <summary>
    /// When this user joined the Space
    /// </summary>
    public DateTime JoinedAt { get; set; }

    /// <summary>
    /// Navigation property to the Space
    /// </summary>
    public virtual Space Space { get; set; } = null!;

    /// <summary>
    /// Navigation property to the user
    /// </summary>
    public virtual AppUser User { get; set; } = null!;
}

/// <summary>
/// Defines role types for Space members (Space-level roles)
/// Space 내 사용자 역할 타입
/// </summary>
public enum SpaceRole
{
    /// <summary>
    /// Member: Regular member with basic access
    /// 일반 멤버 (Closed Space 접근용)
    /// </summary>
    Member = 1,

    /// <summary>
    /// Editor: Can publish all content in the Space (used in Moderated type)
    /// 편집자 (Space 내 모든 콘텐츠 발행 가능, Moderated Type에서 사용)
    /// </summary>
    Editor = 2,

    /// <summary>
    /// Administrator: Can manage Space settings and members
    /// 관리자 (Space 설정, 멤버 관리)
    /// </summary>
    Administrator = 3
}

/// <summary>
/// Defines the access and publishing policy types for Spaces
/// Space의 접근 및 발행 정책 타입
/// </summary>
public enum SpaceType
{
    /// <summary>
    /// Open: All authenticated users can create and publish content
    /// 개방형 자유 게시판: 모든 인증된 사용자가 작성 및 발행 가능
    /// </summary>
    Open = 1,

    /// <summary>
    /// Moderated: All authenticated users can create, but only Space Editors can publish
    /// 개방형 승인 게시판: 모든 인증된 사용자가 작성 가능, Space Editor만 발행 가능
    /// </summary>
    Moderated = 2,

    /// <summary>
    /// Closed: Only Space Members can create and publish content
    /// 폐쇄형 멤버 전용 게시판: Space Member만 작성 및 발행 가능
    /// </summary>
    Closed = 3
}

/// <summary>
/// Represents a tag entity for content categorization
/// </summary>
public class Tag
{
    /// <summary>
    /// EF Core constructor
    /// </summary>
    private Tag()
    {
        // Required for EF Core
    }

    /// <summary>
    /// Domain constructor for creating a new tag
    /// </summary>
    public Tag(string name, string description, int spaceId)
    {
        Name = name;
        Description = description;
        SpaceId = spaceId;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Unique identifier for the tag
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Display name of the tag
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Description of the tag's purpose or usage
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Space ID
    /// </summary>
    public int SpaceId { get; private set; } = 1;

    /// <summary>
    /// Timestamp when the tag was created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    // Private backing field for encapsulation
    private readonly List<ContentTag> _contentTags = new();

    /// <summary>
    /// Contents that use this tag
    /// </summary>
    public IEnumerable<Content> Contents => _contentTags.Select(ct => ct.Content);

    /// <summary>
    /// Navigation property to the space
    /// </summary>
    public virtual Space? Space { get; private set; }

    // Domain methods
    /// <summary>
    /// Updates tag information
    /// </summary>
    public void Update(string name, string description, int spaceId)
    {
        Name = name;
        Description = description;
        SpaceId = spaceId;
    }
}

/// <summary>
/// Represents a topic entity for content thematic categorization
/// Topics describe the subject matter or theme of content (e.g., "AI", "Cloud Computing", "Design Patterns")
/// </summary>
public class Topic
{
    // Private backing field for encapsulation
    private readonly List<ContentTopic> _contentTopics = new();

    /// <summary>
    /// EF Core constructor
    /// </summary>
    private Topic()
    {
        // Required for EF Core
    }

    /// <summary>
    /// Domain constructor for creating a new topic
    /// </summary>
    public Topic(string name, string description, int spaceId)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException("Topic name cannot be empty", nameof(name));

        Name = name;
        Description = description ?? string.Empty;
        SpaceId = spaceId;
        CreatedAt = DateTime.UtcNow;
    }

    /// <summary>
    /// Unique identifier for the topic
    /// </summary>
    public long Id { get; private set; }

    /// <summary>
    /// Display name of the topic
    /// </summary>
    public string Name { get; private set; } = string.Empty;

    /// <summary>
    /// Description of the topic's scope or theme
    /// </summary>
    public string Description { get; private set; } = string.Empty;

    /// <summary>
    /// Space ID
    /// </summary>
    public int SpaceId { get; private set; } = 1;

    /// <summary>
    /// Timestamp when the topic was created
    /// </summary>
    public DateTime CreatedAt { get; private set; }

    /// <summary>
    /// Contents that are associated with this topic
    /// </summary>
    public IEnumerable<Content> Contents => _contentTopics.Select(ct => ct.Content);

    /// <summary>
    /// Navigation property to the space
    /// </summary>
    public virtual Space? Space { get; private set; }
}
