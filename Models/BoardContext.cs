using Microsoft.EntityFrameworkCore;

namespace idiot_chess.Models
{
    public class BoardContext : DbContext
    {
        public BoardContext(DbContextOptions<BoardContext> options)
            : base(options)
        {
        }
        
        public DbSet<ChessBoard> ChessBoards { get; set; }
    }
}