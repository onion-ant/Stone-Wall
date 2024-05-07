export const API_URL = 'https://picsum.photos';

export function ITEMS_GET() {
  return {
    url: API_URL + '/300',
    options: {
      method: 'GET',
    },
  };
}
