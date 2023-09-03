import { TestBed } from '@angular/core/testing';
import { UrlBuilderService } from './url-builder.service';

describe('UrlBuilderService', () => {
  let urlBuilderService: UrlBuilderService;

  beforeEach(() => {
    TestBed.configureTestingModule({
      providers: [
        { provide: 'BASE_URL', useValue: 'TEST_BASE_URL/', deps: [] }
      ]
    });
  });

  it('should build url', () => {
    urlBuilderService = TestBed.inject(UrlBuilderService);

    expect(urlBuilderService.build('api/plans')).toBe('TEST_BASE_URL/api/plans');
  });
});

