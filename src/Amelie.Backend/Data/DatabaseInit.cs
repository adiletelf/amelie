using Amelie.Backend.Models;
using Microsoft.EntityFrameworkCore;

namespace Amelie.Backend.Data;

public static class DatabaseInit
{
    public static void EnsureDatabaseCreated(this IServiceProvider provider)
    {
        using var scope = provider.CreateScope();
        using var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();

        //context.Database.EnsureDeleted();
        context.Database.EnsureCreated();
        Seed(context);
    }

    private static void Migrate(AppDbContext context)
    {
        if (context.Database.GetPendingMigrations().Any())
        {
            context.Database.Migrate();
        }
    }

    private static void Seed(AppDbContext context)
    {
        var rowling = new Author { Name = "J. K. Rowling" };
        var pamPollack = new Author { Name = "Pam Pollack" };
        var megBelviso = new Author { Name = "Meg Belviso" };
        var whoHQ = new Author { Name = "Who HQ" };
        var philipErrington = new Author { Name = "Philip W. Errington" };
        var philipNel = new Author { Name = "Philip Nel" };
        var marcShapiro = new Author { Name = "Marc Shapiro" };
        var colleteenSexton = new Author { Name = "Colleen A. Sexton" };
        var epub = new Author { Name = "EPUB 2-3" };

        var books = new List<Book>
        {
            new Book
            {
                Title = "Who Is J.K. Rowling?",
                Publisher = "Penguin",
                Description = "Everyone loves Harry Potter. Now kids can learn about Harry's creator! In 1995, on a four-hour-delayed train from Manchester to London, J. K. Rowling conceived of the idea of a boy wizard named Harry Potter. Upon arriving in London, she began immediately writing the first book in the saga. Rowling's true-life, rags-to-riches story is as compelling as the world of Hogwarts that she created. This biography details not only Rowling's life and her love of literature but the story behind the creation of a modern classic.",
                Authors = new List<Author>
                {
                    pamPollack,
                    megBelviso,
                    whoHQ,
                },
            },
            new Book
            {
                Title = "The Ickabog",
                Publisher = "Scholastic Incorporated",
                Description = "As the legend of the fearsome Ickabog spreads terror in the peaceful kingdom of Cornucopia, best friends Bert and Daisy set out to discover the truth and bring happiness back to the kingdom.",
                Authors = new List<Author>
                {
                    rowling,
                },
            },
            new Book
            {
                Title = "Conversations with J. K. Rowling",
                Publisher = "Book Wholesalers",
                Description = "The author of the Harry Potter books discusses her childhood, her writing career, and the publishing phenomenon she has created.",
                Authors = new List<Author>
                {
                    rowling,
                },
            },
            new Book
            {
                Title = "Very Good Lives",
                Publisher = "Little, Brown",
                Description = "J.K. Rowling, one of the world's most inspiring writers, shares her wisdom and advice. In 2008, J.K. Rowling delivered a deeply affecting commencement speech at Harvard University. Now published for the first time in book form, VERY GOOD LIVES presents J.K. Rowling's words of wisdom for anyone at a turning point in life. How can we embrace failure? And how can we use our imagination to better both ourselves and others? Drawing from stories of her own post-graduate years, the world famous author addresses some of life's most important questions with acuity and emotional force. Sales of VERY GOOD LIVES will benefit both Lumos, a non-profit international organization founded by J.K. Rowling, which works to end the institutionalization of children around the world, and university-wide financial aid at Harvard University.",
                Authors = new List<Author>
                {
                    rowling
                },
            },
            new Book
            {
                Title = "JK Rowling's Harry Potter Novels",
                Publisher = "A&C Black",
                Description = "Explores the themes found in the novels, provides information about reviews of the novels, and includes information about the life of J.K. Rowling.",
                Authors = new List<Author>
                {
                    philipNel,
                },
            },
            new Book
            {
                Title = "J. K. Rowling: New and Revised",
                Publisher = "Macmillan",
                Description = "Celebrates the life and work of J.K. Rowling, and provides answers to questions regarding her ideas for the Harry Potter series, the characters she identifies with, and how her children feel about their famous mother.",
                Authors = new List<Author>
                {
                    marcShapiro,
                },
            },
            new Book
            {
                Title = "The Casual Vacancy",
                Publisher = "Back Bay Books",
                Description = "A big novel about a small town... When Barry Fairbrother dies in his early forties, the town of Pagford is left in shock. Pagford is, seemingly, an English idyll, with a cobbled market square and an ancient abbey, but what lies behind the pretty fa�ade is a town at war. Rich at war with poor, teenagers at war with their parents, wives at war with their husbands, teachers at war with their pupils...Pagford is not what it first seems. And the empty seat left by Barry on the parish council soon becomes the catalyst for the biggest war the town has yet seen. Who will triumph in an election fraught with passion, duplicity, and unexpected revelations? A big novel about a small town, The Casual Vacancy is J.K. Rowling's first novel for adults. It is the work of a storyteller like no other.",
                Authors = new List<Author>
                {
                    rowling,
                },
            },
            new Book
            {
                Title = "J. K. Rowling",
                Publisher = "Twenty-First Century Books",
                Description = "Presents a biography of celebrated author, J.K. Rowling, and chronicles her life, personal and professional challenges and achievements, and how she rose from poverty to eventually set records in the publishing industry with her Harry Potter series.",
                Authors = new List<Author>
                {
                    colleteenSexton,
                },
            },
            new Book
            {
                Title = "J.K. Rowling",
                Publisher = "Infobase Learning",
                Description = "CHBiographies",
                Authors = new List<Author>
                {
                    epub,
                },
            },
            new Book
            {
                Title = "J.K. Rowling: A Bibliography",
                Publisher = "Bloomsbury Publishing",
                Description = "This is the definitive bibliography of the writings of J. K. Rowling. In addition to bibliographical details of each edition of all her books, pamphlets and original contributions to published works, there is detailed information on the publishing history of her work, including fascinating extracts from correspondence, and information on Rowling at auction. This edition has been fully revised and updated to include over 50 new editions published since 2013, including the newly jacketed 2014 children's editions of the Harry Potter books as well as the 2015 illustrated edition of Harry Potter and the Philosopher's Stone. The works of Robert Galbraith are also included.",
                Authors = new List<Author>
                {
                    philipErrington,
                },
            },
        };

        context.Books.AddRange(books);
        context.SaveChanges();
    }
}
