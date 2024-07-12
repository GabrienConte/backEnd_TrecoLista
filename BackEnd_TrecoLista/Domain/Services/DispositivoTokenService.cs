using AutoMapper;
using BackEnd_TrecoLista.Domain.DTOs.DispositivoToken;
using BackEnd_TrecoLista.Domain.Model;
using BackEnd_TrecoLista.Domain.Services.Interfaces;
using BackEnd_TrecoLista.Infraestrutura.FCM;
using BackEnd_TrecoLista.Infraestrutura.Repository.Interfaces;
using Microsoft.EntityFrameworkCore;
using System.Text;

namespace BackEnd_TrecoLista.Domain.Services
{
    public class DispositivoTokenService : IDispositivoTokenService
    {
        private readonly IDispositivoTokenRepository _dispositivoTokenRepository;
        private readonly IFCMService _fcmService;
        private readonly IMapper _mapper;

        public DispositivoTokenService(IDispositivoTokenRepository dispositivoTokenRepository, IFCMService fcmService, IMapper mapper)
        {
            _dispositivoTokenRepository = dispositivoTokenRepository;
            _fcmService = fcmService;
            _mapper = mapper;
        }

        public async Task<DispositivoTokenDto> GetByIdAsync(int id)
        {
            var dispositivoToken = await _dispositivoTokenRepository.GetByIdAsync(id);
            return _mapper.Map<DispositivoTokenDto>(dispositivoToken);
        }

        public async Task<IEnumerable<DispositivoTokenDto>> GetAllAsync()
        {
            var dispositivoTokens = await _dispositivoTokenRepository.GetAllAsync();
            return _mapper.Map<IEnumerable<DispositivoTokenDto>>(dispositivoTokens);
        }

        public async Task AddOrUpdateDeviceTokenAsync(DispositivoTokenCreateDto dispositivoTokenCreateDto)
        {
            var dispositivoTokenLista = await _dispositivoTokenRepository.GetAllAsync();
            var dispositivoToken =  dispositivoTokenLista
                .FirstOrDefault(dt => dt.UsuarioId == dispositivoTokenCreateDto.UsuarioId && dt.Token == dispositivoTokenCreateDto.Token);

            if (dispositivoToken == null)
            {
                dispositivoToken = _mapper.Map<DispositivoToken>(dispositivoTokenCreateDto);
                var addedDispositivoToken = await _dispositivoTokenRepository.AddAsync(dispositivoToken);
            }
            else
            {
                dispositivoToken = _mapper.Map(dispositivoTokenCreateDto, dispositivoToken);
                var updatedDispositivoToken = await _dispositivoTokenRepository.UpdateAsync(dispositivoToken);
            }
        }

        private async Task<DispositivoTokenDto> AddAsync(DispositivoTokenCreateDto dispositivoTokenCreateDto)
        {
            var dispositivoToken = _mapper.Map<DispositivoToken>(dispositivoTokenCreateDto);
            var addedDispositivoToken = await _dispositivoTokenRepository.AddAsync(dispositivoToken);
            return _mapper.Map<DispositivoTokenDto>(addedDispositivoToken);
        }

        private async Task<DispositivoTokenDto> UpdateAsync(int id, DispositivoTokenCreateDto dispositivoTokenCreateDto)
        {
            var dispositivoToken = await _dispositivoTokenRepository.GetByIdAsync(id);
            if (dispositivoToken == null) return null;

            _mapper.Map(dispositivoTokenCreateDto, dispositivoToken);
            var updatedDispositivoToken = await _dispositivoTokenRepository.UpdateAsync(dispositivoToken);
            return _mapper.Map<DispositivoTokenDto>(updatedDispositivoToken);
        }

        public async Task<bool> DeleteAsync(int id)
        {
            return await _dispositivoTokenRepository.DeleteAsync(id);
        }

        public async Task<string> GetTokenByUserIdAsync(int userId)
        {
            return await _dispositivoTokenRepository.GetTokenByUserIdAsync(userId);
        }

        public async Task<List<string>> GetTokensByUserIdsAsync(List<int> userIds)
        {
            return await _dispositivoTokenRepository.GetTokensByUserIdsAsync(userIds);
        }

        public async Task EnviarNotificacaoMudouPrecoToUserAsync(int userId, string produtoDescricao, decimal novoPreco, decimal antigoPreco)
        {
            var token = await _dispositivoTokenRepository.GetTokenByUserIdAsync(userId);
            if (!string.IsNullOrEmpty(token))
            {
                string title = "Um produto mudou de preço!";
                StringBuilder body = new StringBuilder();
                body.Append(String.Format("O seu favorito {0} teve uma alteração de preço! Preço novo:{1} Preço antigo:{2}",
                    produtoDescricao,
                    novoPreco,
                    antigoPreco));

                await _fcmService.EnviarNotificacaoAsync(token, title, body.ToString());
            }
        }

        public async Task EnviarNotificacaoMudouPrecoToMultipleUsersAsync(List<int> userIds, string produtoDescricao, decimal novoPreco, decimal antigoPreco)
        {
            throw new NotImplementedException();
        }
    }

}
