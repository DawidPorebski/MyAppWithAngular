export class MyEntityViewModel {
  public id: number = 0;
  public text: string = '';

  public constructor(response: MyEntityViewModel) {
    this.id = response.id;
    this.text = response.text;
  }
}
