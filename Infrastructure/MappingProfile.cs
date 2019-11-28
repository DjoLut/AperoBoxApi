using AutoMapper;
using AperoBoxApi.Models;
using AperoBoxApi.DTO;

namespace AperoBoxApi.Infrastructure
{
   public class MappingProfile : Profile
    {   
        public MappingProfile()
        {
            CreateMap<Utilisateur, UtilisateurDTO>();
            CreateMap<UtilisateurDTO, Utilisateur>();
            CreateMap<Produit, ProduitDTO>();
            CreateMap<ProduitDTO, Produit>();
            CreateMap<LigneProduit, LigneProduitDTO>();
            CreateMap<LigneProduitDTO, LigneProduit>();
            CreateMap<LigneCommande, LigneCommandeDTO>();
            CreateMap<LigneCommandeDTO, LigneCommande>();
            CreateMap<Commentaire, CommentaireDTO>();
            CreateMap<CommentaireDTO, Commentaire>();
            CreateMap<Commande, CommandeDTO>();
            CreateMap<CommandeDTO, Commande>();
            CreateMap<Box, BoxDTO>();
            CreateMap<BoxDTO, Box>();
            CreateMap<Adresse, AdresseDTO>();
            CreateMap<AdresseDTO, Adresse>();
        }
    }
}