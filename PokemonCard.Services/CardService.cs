
using PokemonCard.Data;
using PokemonCard.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PokemonCard.Services
{
    public class CardService
    {
        private readonly Guid _userId;

        public CardService(Guid userId)
        {
            _userId = userId;
        }

        public bool CreateCard(CardCreate model)
        {
            var entity =
                new Card()
                {
                    OwnerId = _userId,
                    Name = model.Name,
                    SetId = model.SetId,
                    TypeOfCard = model.TypeOfCard,
                    Rarity = model.Rarity,
                    ArtStyle = model.ArtStyle
                };

            using (var ctx = new ApplicationDbContext())
            {
                ctx.Cards.Add(entity);
                return ctx.SaveChanges() == 1;
            }
        } 
        
        public IEnumerable<CardListItem> GetCards()
        {
            using (var ctx = new ApplicationDbContext())
            {
                var query =
                    ctx
                        .Cards
                        .Where(e => e.OwnerId == _userId)
                        .Select(
                            e =>
                                new CardListItem
                                {
                                    Id = e.Id,
                                    Name = e.Name
                                }
                        );

                return query.ToArray();
            }
        }

        public CardDetail GetCardById(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.Id == id );
                var set =
                    ctx.PokemonSets.Single(e => e.SetId == entity.SetId);
                    return
                        new CardDetail
                        {
                            Id = entity.Id,
                            Name = entity.Name,                            
                            Set = set,
                            TypeOfCard = entity.TypeOfCard,
                            IsHolo = entity.IsHolo,
                            ArtStyle = entity.ArtStyle,
                            Rarity = entity.Rarity
                        };
            }
        }
        public bool UpdateCard (CardEdit model)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity = ctx.Cards.Single(e => e.Id == model.Id );
                var set =
                    ctx.PokemonSets.Single(e => e.SetId == entity.SetId);

                entity.Name = model.Name;
                entity.SetId = model.SetId;
                entity.Set = set;
                entity.TypeOfCard = model.TypeOfCard;
                entity.IsHolo = model.IsHolo;
                entity.ArtStyle = model.ArtStyle;
                entity.Rarity = model.Rarity;
                entity.OwnerId = model.OwnerId;

                return ctx.SaveChanges() == 1;
            }
        }

        public bool DeleteCard(int id)
        {
            using (var ctx = new ApplicationDbContext())
            {
                var entity =
                    ctx
                        .Cards
                        .Single(e => e.Id == id && e.OwnerId == _userId);

                ctx.Cards.Remove(entity);

                return ctx.SaveChanges() == 1;

            }
        }
    }
}
