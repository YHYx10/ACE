<template>
<body>
  <div :style="'zoom:' + zoomDesign + ''">
  <h1 class="title">
    Creating a character
  </h1>
  <p class="descr">
A brief description of this interface, why and why.
  </p>
  <div class="tabs">
    <ul class="tabs__list">
      <li class="tabs__item"><button class="tabs__btn tabs__btn--active" data-tabs-path="main"
          style="background-image: url('/img/testcust/info-ico.png');background-size: 11px;"></button>
      </li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="family"
          style="background-image: url('/img/testcust/family-ico.png');background-size: 25px;"></button>
      </li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="genetic"
          style="background-image: url('/img/testcust/genetic-ico.png');background-size: 25px;"></button></li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="hair"
          style="background-image: url('/img/testcust/hair-ico.png');background-size: 24px;"></button></li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="brush"
          style="background-image: url('/img/testcust/brush-ico.png');background-size: 24px;"></button></li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="tras"
          style="background-image: url('/img/testcust/tr-ico.png');background-size: 24px;"></button></li>
      <li class="tabs__item"><button class="tabs__btn" data-tabs-path="clothes"
          style="background-image: url('/img/testcust/clothes-ico.png');background-size: 23px;"></button></li>
    </ul>
    <div class="tabs__content tabs__content--active" data-tabs-target="main">
      <div class="content">
        <form class="form">
          <div class="form__body">
            <div class="form__sex">
              <div class="form__box">
         Choose the floor of the character
              </div>
              <div class="form__sex_select">
                <input type="radio" class="sex" id="male" name="sex" />
                <label for="male" id="male">MALE</label>
                <input type="radio" class="sex" id="female" name="sex" />
                <label for="female" id="female">Female</label>
              </div>
            </div>
            <div class="form__name">
              <div class="form__box">
       <h3>
The name of the character
                </h3>
              </div>
              <div class="form__data">
                <input type="text" class="form__input" placeholder="ROMARIO">
              </div>
            </div>
            <div class="form__subname">
              <div class="form__box">
                <h3>
                  The surname of the character
                </h3>
              </div>
              <div class="form__data">
                <input type="text" class="form__input" placeholder="RICHARDSON">
              </div>
            </div>

            <div class="form__parents">
              <div class="form__parents_select">
                <input type="radio" class="p-sex" id="p-male" name="p-sex" />
                <label for="p-male" id="p-male">
                  <h3>Parents -<br> Fathers</h3>
                </label>
                <input type="radio" class="p-sex" id="p-female" name="p-sex" />
                <label for="p-female" id="p-female">
                  <h3>Parents -<br> Mother</h3>
                </label>
              </div>
              <div class="form__parents_content">
                <input type="radio" class="parent-fa" id="parent-f1" name="parent-fa" />
                <label for="parent-f1" id="parent-f1"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f2" name="parent-fa" />
                <label for="parent-f2" id="parent-f2"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f3" name="parent-fa" />
                <label for="parent-f3" id="parent-f3"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f4" name="parent-fa" />
                <label for="parent-f4" id="parent-f4"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f5" name="parent-fa" />
                <label for="parent-f5" id="parent-f5"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f6" name="parent-fa" />
                <label for="parent-f6" id="parent-f6"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f7" name="parent-fa" />
                <label for="parent-f7" id="parent-f7"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f8" name="parent-fa" />
                <label for="parent-f8" id="parent-f8"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
                <input type="radio" class="parent-fa" id="parent-f9" name="parent-fa" />
                <label for="parent-f9" id="parent-f9"><img src="/img/testcust/female-ico.png" alt="" class="p-img"></label>
              </div>
            </div>

            <div class="form__face">
              <div class="form__box">
                <h3>
                  Shape<br> faces
                </h3>
              </div>

              <div class="form__value">
                <input type="range" value="70" min="0" max="100" oninput="rangevalue.value=value" />
                <output id="rangevalue">70</output>
              </div>
            </div>

            <div class="form__leather">
              <div class="form__box">
                <h3>
                  Shade<br> skin
                </h3>
              </div>

              <div class="form__value">
                <input type="range" value="70" min="0" max="100" oninput="rangevalue.value=value" />
                <output id="rangevalue">70</output>
              </div>
            </div>

            <div class="form__color">
              <div class="form__box">
                <h3>
                  Color<br> something
                </h3>
              </div>

              <div class="form__selector">
                <input type="radio" class="color selected" id="color-1" name="color" />
                <!-- selected - Selected color -->
                <label for="color-1" id="color-1"></label>
                <input type="radio" class="color" id="color-2" name="color" />
                <label for="color-2" id="color-2"></label>
                <input type="radio" class="color" id="color-3" name="color" />
                <label for="color-3" id="color-3"></label>
                <input type="radio" class="color" id="color-4" name="color" />
                <label for="color-4" id="color-4"></label>
                <input type="radio" class="color" id="color-5" name="color" />
                <label for="color-5" id="color-5"></label>
                <input type="radio" class="color" id="color-6" name="color" />
                <label for="color-6" id="color-6"></label>
                <input type="radio" class="color" id="color-7" name="color" />
                <label for="color-7" id="color-7"></label>
                <input type="radio" class="color" id="color-8" name="color" />
                <label for="color-8" id="color-8"></label>
                <input type="radio" class="color" id="color-9" name="color" />
                <label for="color-9" id="color-9"></label>
                <input type="radio" class="color" id="color-10" name="color" />
                <label for="color-10" id="color-10"></label>
                <input type="radio" class="color" id="color-11" name="color" />
                <label for="color-11" id="color-11"></label>
                <input type="radio" class="color" id="color-12" name="color" />
                <label for="color-12" id="color-12"></label>
                <input type="radio" class="color" id="color-13" name="color" />
                <label for="color-13" id="color-13"></label>
                <input type="radio" class="color" id="color-14" name="color" />
                <label for="color-14" id="color-14"></label>
                <input type="radio" class="color" id="color-15" name="color" />
                <label for="color-15" id="color-15"></label>
                <input type="radio" class="color" id="color-16" name="color" />
                <label for="color-16" id="color-16"></label>
                <input type="radio" class="color" id="color-17" name="color" />
                <label for="color-17" id="color-17"></label>
                <input type="radio" class="color" id="color-18" name="color" />
                <label for="color-18" id="color-18"></label>
                <!--<input type="color" id="color1" name="color1" value="#FFFFFF" disabled>
                <input type="color" id="color2" name="color2" value="#FF8F8F" disabled>
                <input type="color" id="color3" name="color3" value="#FF2323" disabled>
                <input type="color" id="color4" name="color4" value="#FFCD4D" disabled>
                <input type="color" id="color5" name="color5" value="#7BFF4D" disabled>
                <input type="color" id="color6" name="color6" value="#4DFF74" disabled>
                <input type="color" id="color7" name="color7" value="#4DFFD4" disabled>
                <input type="color" id="color8" name="color8" value="#4D69FF" disabled>
                <input type="color" id="color9" name="color9" value="#9B4DFF" disabled>
                <input type="color" id="color10" name="color10" value="#0C0C0C" disabled>
                <input type="color" id="color11" name="color11" value="#FF8717" disabled>
                <input type="color" id="color12" name="color12" value="#FFF617" disabled>
                <input type="color" id="color13" name="color13" value="#61FF17" disabled>
                <input type="color" id="color14" name="color14" value="#17F1FF" disabled>
                <input type="color" id="color15" name="color15" value="#172FFF" disabled>
                <input type="color" id="color16" name="color16" value="#6117FF" disabled>
                <input type="color" id="color17" name="color17" value="#D117FF" disabled>
                <input type="color" id="color18" name="color18" value="#FF175D" disabled>-->
              </div>
            </div>
          </div>
          <div class="form__footer">
            <button class="btn btn--save">Save the character</button>
          </div>
        </form>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="family">
      <div class="content">
        <h1 style="color: #fff;">1322312312323</h1>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="genetic">
      <div class="content">
        <h1 style="color: #fff;">123124556</h1>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="hair">
      <div class="content">
        <h1>test</h1>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="brush">
      <div class="content">
        <h1>test</h1>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="tras">
      <div class="content">
        <h1>test</h1>
      </div>
    </div>
    <div class="tabs__content" data-tabs-target="clothes">
      <div class="content">
        <h1>test</h1>
      </div>
    </div>
  </div>
  </div>
</body>
</template>

<script>
export default {
  name: 'TestMenu',
  data() {
    return {
      zoomDesign: 1,
    }
  },
  mounted(){
    this.handleResize();
    window.addEventListener('resize', this.handleResize);
  },
  beforeDestroy(){
    window.removeEventListener('resize', this.handleResize);
  },
  methods: {
    handleResize() {
        let zoomCountOne = document.body.clientWidth / 1920;//1920;
        let zoomCountTwo = document.body.clientHeight / 1080;//1080;

        if(zoomCountOne < zoomCountTwo) this.zoomDesign = zoomCountOne;
        else this.zoomDesign = zoomCountTwo;
    }
  }
}
</script>

<style lang="scss" scoped>
body {
  margin: 0;
  font-family: 'Akrobat';
  overflow-anchor: none;
  background-image: url('/img/testcust/bg-home.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  background-position: center center;
  width: 100vw;
  height: 100vh;
}

ul {
  list-style-type: none;
}

.btn {
  cursor: pointer;
  transition: background-color 0.3s ease-in-out;

  &--save {
    border: none;
    font-family: 'Akrobat';
    font-weight: 600;
    font-size: 1.5069967707212055vh;
    line-height: 1.8299246501614639vh;
    padding: 2.183423035522067vh 0 2.1910656620021527vh 0;
    width: 100%;
    text-transform: uppercase;
    color: #FFFFFF;
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);

    &:hover {
      background: linear-gradient(180deg, #b41119 0%, #690b10 100%);
    }
  }
}

.btn:hover {
  transition: background-color 0.3s ease-in-out;
}

.btn:disabled {
  opacity: 0.4;
  pointer-events: none;
}

.title {
  font-weight: 800;
  font-size: 5.166846071044134vh;
  line-height: 5.243272vh;
  background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
  -webkit-background-clip: text;
  -webkit-text-fill-color: transparent;
  background-clip: text;
  text-fill-color: transparent;
  text-shadow: 0 0 12.055974165769644vh rgba(255, 255, 255, 0.25);
  padding-left: 5.166846071044134vh;
}

.descr {
  color: #fff;
  font-weight: 600;
  font-size: 1.6146393972012918vh;
  line-height: 1.2375672766415502vh;
  text-transform: uppercase;
  padding-left: 5.166846071044134vh;
  margin-bottom: 1.2146393972012918vh;
}

.tabs {
  height: 100%;
  padding-left: 5.166846071044134vh;

  &__list {
    list-style-type: none;
    padding: 0;
    margin: 0;
    display: flex;
    align-items: center;
  }

  &__btn {
    border: none;
    display: flex;
    align-items: center;
    justify-content: center;
    font-family: 'Akrobat';
    font-weight: 700;
    background: rgba(255, 255, 255, 0.01);
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    border-radius: 0.21528525296017223vh;
    width: 5.920344456404736vh;
    height: 5.920344456404736vh;
    cursor: pointer;
    margin-right: 1.0764262648008611vh;
    background-repeat: no-repeat;
    background-position: center center;
    transition: background-color 0.3s ease-in-out, color 0.3s ease-in-out;

    &:hover {
      background-color: #09090A;
      color: rgb(236, 236, 236);
    }

    img {
      max-width: 2.9063509149623252vh;
      max-height: 3.013993541442411vh;
    }
  }

  &__btn--active {
    background: #301934 ;
    border: 0.10764262648008611vh solid #a7151c;
    transition: background-color 0.3s ease-in-out, color 0.3s ease-in-out;
    pointer-events: none;
    background-repeat: no-repeat;
    background-position: center center;
  }

  &__content {
    display: none;
  }

  &__content--active {
    display: flex;
  }
}

.content {
  padding: 0.7764262648008611vh 0 0 0;
  width: 47.90096878363832vh;
  color: #fff;
}

.form {

  &__sex {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__name {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__subname {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__parents {
    display: flex;
    margin-bottom: 0.6534983853606028vh;

    &_select {
      margin-right: 1.2917115177610334vh;
    }

    &_content {
      display: grid;
      grid-template-columns: 9.041980624327234vh 9.041980624327234vh 9.041980624327234vh;
      column-gap: 1.0764262648008611vh;
      row-gap: 0.6764262648008611vh;
    }
  }

  &__face {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__leather {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__color {
    display: flex;
    align-items: center;
    margin-bottom: 0.6534983853606028vh;
  }

  &__selector {
    display: grid;
    justify-content: center;
    align-items: center;
    grid-template-columns: 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh 2.416576964477933vh;
    column-gap: 0.5974165769644779vh;
    row-gap: 0.5382131324004306vh;
    background-image: url('/img/testcust/bg-input.png');
    padding: 1.1840688912809472vh 1.566200215285253vh 1.1356297093649086vh 1.1840688912809472vh;
    width: 29.278794402583422vh;
    height: 7.750269106566201vh;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
  }

  &__value {
    display: flex;
    align-items: center;
    background-image: url('/img/testcust/bg-input.png');
    padding: 2.7063509149623252vh 1.7222820236813778vh 3.013993541442411vh 3.567491926803014vh;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    width: 29.378794402583422vh;
  }

  &__input {
    font-family: 'Akrobat';
    width: 29.278794402583422vh;
    height: 7.750269106566201vh;
    padding: 2.9063509149623252vh 0 3.013993541442411vh 0;
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    background: url('/img/testcust/bg-input.png');
    text-align: center;
    background-size: cover;
    color: #fff;
    font-weight: 600;
    font-size: 1.5069967707212055vh;
    line-height: 1.8299246501614639vh;

    &::placeholder {
      color: inherit;
      font-weight: inherit;
      font-size: inherit;
      line-height: inherit;
    }
  }

  &__box {
    border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
    background-image: url('/img/testcust/bg-select.png');
    padding: 2.3681377825618943vh 0 2.3681377825618943vh 3.552206673842842vh;
    width: 17.222820236813778vh;
    height: 7.750269106566201vh;
    color: #fff;
    font-weight: 700;
    font-size: 1.2917115177610334vh;
    line-height: 1.5069967707212055vh;
    text-transform: uppercase;
    margin-right: 1.2917115177610334vh;
    position: relative;

    &::after {
      content: '';
      position: absolute;
      width: 2.798708288482239vh;
      height: 0.21528525296017223vh;
      top: 50%;
      left: 0;
      background: #FFFFFF;
      box-shadow: 0 0 1.5069967707212055vh rgba(255, 255, 255, 0.55);
      transform: rotate(-90deg);
    }

    h3 {
      font-family: 'Akrobat';
      color: #fff;
      font-weight: 700;
      font-size: 1.2917115177610334vh;
      line-height: 1.5069967707212055vh;
      max-width: 7.35629709364909vh;
    }
  }
}

.color+label {
  margin: 0;
  border: none;
  width: 2.416576964477933vh;
  height: 2.416576964477933vh;
  cursor: pointer;
  transition: .3s all;
}

.selected:checked+label {
  transition: .3s all;
  border: 0.25764262648008611vh solid #000000;
}

input[id="color-1"]+label {
  background: #fff;
}

input[id="color-2"]+label {
  background: #FF8F8F;
}

input[id="color-3"]+label {
  background: #FF2323;
}

input[id="color-4"]+label {
  background: #FFCD4D;
}

input[id="color-5"]+label {
  background: #7BFF4D;
}

input[id="color-6"]+label {
  background: #4DFF74;
}

input[id="color-7"]+label {
  background: #4DFFD4;
}

input[id="color-8"]+label {
  background: #4D69FF;
}

input[id="color-9"]+label {
  background: #9B4DFF;
}

input[id="color-10"]+label {
  background: #0C0C0C;
}

input[id="color-11"]+label {
  background: #FF8717;
}

input[id="color-12"]+label {
  background: #FFF617;
}

input[id="color-13"]+label {
  background: #61FF17;
}

input[id="color-14"]+label {
  background: #17F1FF;
}

input[id="color-15"]+label {
  background: #172FFF;
}

input[id="color-16"]+label {
  background: #6117FF;
}

input[id="color-17"]+label {
  background: #D117FF;
}

input[id="color-18"]+label {
  background: #FF175D;
}

input[type="radio"] {
  display: none;
  transition: .3s all;
}

input[type="radio"]:checked+label {
  color: #00FF38;
}

.sex:checked+label {
  position: relative;

  &::after {
    content: '';
    position: absolute;
    bottom: 0.6458557588805167vh;
    left: 50%;
    transform: translateX(-50%);
    width: 6.027987082884822vh;
    height: 0.21528525296017223vh;
    background: #1AFF4C;
    box-shadow: 0 -0.21528525296017223vh 2.2604951560818085vh #00FF38;
  }
}

.p-sex:checked+label {
  color: #5CFF80 !important;
  border: 0.10764262648008611vh solid #5CFF80;

  &::after {
    background: #5CFF80;
    border: 0.10764262648008611vh solid rgba(92, 255, 128, 0.09);
    box-shadow: 0 0 1.5069967707212055vh rgba(92, 255, 128, 0.55);
    transform: rotate(-90deg);
  }
}

.parent-fa:checked+label {
  border: 0.10764262648008611vh solid #19FF4C
}

label[id="male"],
label[id="female"] {
  font-family: 'Akrobat';
  color: #fff;
  font-weight: 600;
  height: 7.750269106566201vh;
  font-size: 1.5069967707212055vh;
  line-height: 1.8299246501614639vh;
  text-transform: uppercase;
  display: block;
  padding: 2.9063509149623252vh 4.090419806243272vh 3.013993541442411vh 3.982777179763186vh;
  float: left;
  border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
  background-image: url('/img/testcust/bg-select.png');
  cursor: pointer;
  transition: .3s all;
}

label h3 {
  font-family: 'Akrobat';
  color: #fff;
  font-weight: 700;
  font-size: 1.2917115177610334vh;
}

label[id="parent-f1"],
label[id="parent-f2"],
label[id="parent-f3"],
label[id="parent-f4"],
label[id="parent-f5"],
label[id="parent-f6"],
label[id="parent-f7"],
label[id="parent-f8"],
label[id="parent-f9"] {
  overflow: hidden;
  width: 9.041980624327234vh;
  height: 9.041980624327234vh;
  display: flex;
  align-items: center;
  justify-content: center;
  border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
  padding: 0.8611410118406889vh 0.5382131324004306vh 0 0.43057050592034446vh;
  cursor: pointer;
  transition: .3s all;
}

label[id="p-male"],
label[id="p-female"] {
  display: flex;
  color: #fff;
  font-weight: 600;
  width: 17.330462863293864vh;
  height: 7.750269106566201vh;
  font-size: 1.5069967707212055vh;
  line-height: 1.6299246501614639vh;
  text-transform: uppercase;
  padding: 2.3681377825618943vh 0 2.1681377825618943vh 3.552206673842842vh;
  border: 0.10764262648008611vh solid rgba(255, 255, 255, 0.09);
  background-image: url('/img/testcust/bg-select.png');
  cursor: pointer;
  position: relative;
  transition: .3s all;

  &::after {
    content: '';
    position: absolute;
    width: 2.798708288482239vh;
    height: 0.21528525296017223vh;
    top: 50%;
    left: 0;
    background: #FFFFFF;
    box-shadow: 0 0 1.5069967707212055vh rgba(255, 255, 255, 0.55);
    transform: rotate(-90deg);
  }
}

label[id="male"] {
  margin-right: 1.2917115177610334vh;
}

label[id="p-male"] {
  margin-bottom: 1.0764262648008611vh;
}

label[id="male"]:hover,
label[id="female"]:hover {
  color: #00FF38;
}

/ * Slider */
output {
  font-family: 'Akrobat';
  color: #00FF38;
  text-transform: uppercase;
  font-weight: 700;
  font-size: 1.5069967707212055vh;
  line-height: 1.8299246501614639vh;
}

input[type="number"] {
  font-family: 'Akrobat';
  width: 4.3057050592034445vh;
  padding: 0.43057050592034446vh 0.5382131324004306vh;
  border: 0.10764262648008611vh solid #bbb;
  border-radius: 0.32292787944025836vh;
}

input[type="range"] {
  font-family: 'Akrobat';
  -webkit-appearance: none;
  margin-right: 1.6146393972012918vh;
  width: 19.91388589881593vh;
  height: 0.33057050592034446vh;
  background: rgba(255, 255, 255, 0.25);
  background-image: linear-gradient(#1AFF4C, #1AFF4C);
  background-size: 70% 100%;
  background-repeat: no-repeat;
}

/* Input Thumb */
input[type="range"]::-webkit-slider-thumb {
  -webkit-appearance: none;
  height: 0.8611410118406889vh;
  width: 0.8611410118406889vh;
  background: #FFFFFF;
  cursor: ew-resize;
  box-shadow: 0 0 0.21528525296017223vh 0 #555;
  transition: background .3s ease-in-out;
}

input[type="range"]::-moz-range-thumb {
  -webkit-appearance: none;
  height: 0.8611410118406889vh;
  width: 0.8611410118406889vh;
  background: #FFFFFF;
  cursor: ew-resize;
  box-shadow: 0 0 0.21528525296017223vh 0 #555;
  transition: background .3s ease-in-out;
}

input[type="range"]::-ms-thumb {
  -webkit-appearance: none;
  height: 0.8611410118406889vh;
  width: 0.8611410118406889vh;
  background: #FFFFFF;
  cursor: ew-resize;
  box-shadow: 0 0 0.21528525296017223vh 0 #555;
  transition: background .3s ease-in-out;
}

input[type="range"]::-webkit-slider-thumb:hover {
  background: #FFFFFF;
}

input[type="range"]::-moz-range-thumb:hover {
  background: #FFFFFF;
}

input[type="range"]::-ms-thumb:hover {
  background: #FFFFFF;
}

/* Input Track */
input[type=range]::-webkit-slider-runnable-track {
  -webkit-appearance: none;
  box-shadow: none;
  border: none;
  background: transparent;
}

input[type=range]::-moz-range-track {
  -webkit-appearance: none;
  box-shadow: none;
  border: none;
  background: transparent;
}

input[type="range"]::-ms-track {
  -webkit-appearance: none;
  box-shadow: none;
  border: none;
  background: transparent;
}

.p-img {
  width: 8.073196986006458vh;
  height: 8.073196986006458vh;
}
</style>
