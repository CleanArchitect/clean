using Clean.Net;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure;

public class EntityFrameworkRepositoryTests
{
    private readonly EntityFrameworkRepository<TestEntity> repository;

    public EntityFrameworkRepositoryTests()
    {
        var dbContextOptions = new DbContextOptionsBuilder<TestDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;

        var context = new TestDbContext(dbContextOptions);

        repository = new EntityFrameworkRepository<TestEntity>(context, new MockEventBus());
    }

    [Fact]
    public async Task FindAsync_ShouldReturnEntity_WhenEntityExists()
    {
        // arrange
        var testEntity = TestEntity.Create("Test Find");

        await repository
            .Add(testEntity)
            .SaveChangesAsync();

        // act
        var result = await repository.FindAsync(testEntity.Id);

        // assert
        Assert.NotNull(result);
        Assert.Equal(testEntity.Id, result.Id);
        Assert.Equal(testEntity.Name, result.Name);
    }

    [Fact]
    public async Task GetAllAsync_ShouldReturnAllEntities_WhenNoPredicateProvided()
    {
        // arrange
        var testEntities = TestEntity.CreateRange(["First", "Second"]);

        await repository
            .AddRange(testEntities)
            .SaveChangesAsync();

        // act
        var result = await repository.GetAllAsync();

        // assert
        Assert.Equal(testEntities.Length, result.Count());
    }

    [Fact]
    public async Task SingleAsync_ShouldReturnSingleEntity_WhenPredicateMatches()
    {
        // arrange
        var testEntities = TestEntity.CreateRange(["First", "Second"]);

        await repository
            .AddRange(testEntities)
            .SaveChangesAsync();

        // act
        var result = await repository.SingleAsync(entity => entity.Name == testEntities.First().Name);

        // assert
        Assert.NotNull(result);
        Assert.Equal(testEntities.First().Name, result.Name);
    }

    [Fact]
    public async Task FirstAsync_ShouldReturnFirstEntity_WhenPredicateMatches()
    {
        // arrange
        var entityName = "Same";
        var testEntities = TestEntity.CreateRange([entityName, entityName]);

        await repository
            .AddRange(testEntities)
            .SaveChangesAsync();

        // act
        var result = await repository.FirstAsync(entity => entity.Name == entityName);

        // assert
        Assert.NotNull(result);
        Assert.Equal(entityName, result.Name);
    }

    [Fact]
    public async Task AnyAsync_ShouldReturnTrue_WhenEntityMatchesPredicate()
    {
        // arrange
        var testEntity = TestEntity.Create("Test Any");

        await repository
            .Add(testEntity)
            .SaveChangesAsync();

        // act
        var result = await repository.AnyAsync(entity => entity.Name == testEntity.Name);

        // assert
        Assert.True(result);
    }

    [Fact]
    public async Task Add_ShouldAddEntityToDatabase()
    {
        // arrange
        var testEntity = TestEntity.Create("Test Add");

        // act
        await repository
            .Add(testEntity)
            .SaveChangesAsync();

        // assert
        var savedEntity = await repository.FindAsync(testEntity.Id);
        Assert.NotNull(savedEntity);
        Assert.Equal(testEntity.Name, savedEntity.Name);
    }

    [Fact]
    public async Task DeleteAsync_ShouldRemoveEntityFromDatabase()
    {
        // arrange
        var testEntity = TestEntity.Create("Test Delete");

        await repository
            .Add(testEntity)
            .SaveChangesAsync();

        // act
        await repository.DeleteAsync(testEntity.Id);
        await repository.SaveChangesAsync();

        // assert
        var deletedEntity = await repository.FindAsync(testEntity.Id);
        Assert.Null(deletedEntity);
    }

    private class TestDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet<TestEntity> TestEntities { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<TestEntity>()
                .Property(entity => entity.Id)
                .ValueGeneratedOnAdd();
        }
    }

    private class TestEntity : Entity
    {
        public string Name { get; private set; }

        private TestEntity(string name)
        {
            Name = name;
        }

        public static TestEntity Create(string name) =>
            new(name);

        public static TestEntity[] CreateRange(string[] names) =>
           names.Select(name => new TestEntity(name)).ToArray();
    }

    private class MockEventBus : IEventBus
    {
        public Task RaiseEventAsync(params IEvent[] events) => Task.CompletedTask;
    }
}