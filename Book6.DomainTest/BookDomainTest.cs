using Book6;
using MM.ES.Testing;
using MM.ES;
using NUnit.Framework;
using System.Collections.Generic;
using System;
using Book6.Domain;
using Book6.CommandHandler;

public class BookDomainTest : AggregateTestFixture<Book>
{
    [TearDown]
    public void Teardown()
    { }

    private BookHandler commandHandler;
    private Guid id;
    private List<IMessage> givenBooks;
    protected override Dictionary<Guid, IEnumerable<IMessage>> Given()
    {
        id = Guid.NewGuid();
        givenBooks = new List<IMessage>
                {
                    new NewBook
                    {
                        Id= id,
                        Title= "testTitle",
                        IsInWaitlist= false,
                        IsReserved= false,
                    }
                };

        return new Dictionary<Guid, IEnumerable<IMessage>> { { id, givenBooks } };
    }

    protected override void SetupCommandHandler(IRepository repository)
    {
        commandHandler = new BookHandler(repository);
    }

    [Test]
    public void NewBook()
    {
        var command = new NewBook()
        {
            Id = Guid.NewGuid(),
            Title = "Book for test",
            IsInWaitlist = false,
            IsReserved = false,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void UpdateBookSameTitle()
    {
        var command = new UpdateBook()
        {
            Id = Guid.NewGuid(),
            Title = "Book for test",
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);
    }
    [Test]
    public void UpdateBookNotSameId()
    {
        var command = new UpdateBook()
        {
            Id = Guid.NewGuid(),
            Title = "Book for test update",
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);
    }
    public void UpdateBook()
    {
        var command = new UpdateBook()
        {
            Id = id,
            Title = "Book for test update",
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void DeleteBook()
    {
        var command = new DeleteBook()
        {
            Id = id,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void DeleteBookNotSameID()
    {
        var command = new DeleteBook()
        {
            Id = Guid.NewGuid(),
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessExceptionCommand(commandHandler, command);
    }
    [Test]
    public void UpdateBookBookStatus()
    {
        var command = new UpdateBookReservationStatus()
        {
            Id = id,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }
    [Test]
    public void UpdateBookWaitlistStatus()
    {
        var command = new UpdateBookWaitlistStatus()
        {
            Id = id,
        };
        Then.Add(command.Id, new IMessage[] { command });
        ProcessCommand(commandHandler, command);
    }

}