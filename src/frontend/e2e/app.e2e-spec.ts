import { VvfmlPage } from './app.po';

describe('vvfml App', () => {
  let page: VvfmlPage;

  beforeEach(() => {
    page = new VvfmlPage();
  });

  it('should display welcome message', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('Welcome to app!!');
  });
});
