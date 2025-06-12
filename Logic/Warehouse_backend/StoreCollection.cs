using InterfacesDal;
using InterfacesDal.DTOs;

namespace WarehouseBLL
{
    public class StoreCollection
    {
        private readonly IStoreRepository StoreRepository;

        public StoreCollection(IStoreRepository storeRepository)
        {
            this.StoreRepository = storeRepository;
        }
        public async IAsyncEnumerable<Store> GetAllStores()
        {
            IAsyncEnumerable<StoreDTO> stores = StoreRepository.GetAllStores();

            await foreach (StoreDTO store in stores)
            {
                yield return new Store()
                {
                    ID = store.ID,
                    Name = store.Name,
                    Postcode = store.Postcode,
                    Street = store.Street,
                };
            }
        }

        public async Task<Store?> GetStore(int ID)
        {
            Store store = new Store();
            IAsyncEnumerable<StoreDTO> stores = StoreRepository.GetStore(ID);

            await foreach (StoreDTO storeDto in stores)
            {
                store = new Store()
                {
                    ID = storeDto.ID,
                    Name = storeDto.Name,
                    Postcode = storeDto.Postcode,
                    Street = storeDto.Street,
                };
            }
            if (store.Name == null)
            {
                return null;
            }
            return store;
        }
    }
}
