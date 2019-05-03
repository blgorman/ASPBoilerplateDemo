import { BoilerplateDemoTemplatePage } from './app.po';

describe('BoilerplateDemo App', function() {
  let page: BoilerplateDemoTemplatePage;

  beforeEach(() => {
    page = new BoilerplateDemoTemplatePage();
  });

  it('should display message saying app works', () => {
    page.navigateTo();
    expect(page.getParagraphText()).toEqual('app works!');
  });
});
