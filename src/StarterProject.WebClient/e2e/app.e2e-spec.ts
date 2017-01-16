import { StarterPage } from './app.po';

describe('starter App', function() {
  let page: StarterPage;

  beforeEach(() => {
    page = new StarterPage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
