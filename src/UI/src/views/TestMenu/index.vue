<template>
<body>
  <div class="menu-person" :style="'zoom:' + zoomDesign + ''">
    <header class="header">
      <div class="header__container container">
        <div class="header__text">
          <h1 class="header__title">
           Character menu
          </h1>
          <p class="header__descr">
         A brief description of this interface, why and why.
          </p>
        </div>

        <div class="header__tabs tabs">
          <div class="tabs__list">
            <button class="tabs__button tabs__button-active">Statistics</button>
            <button class="tabs__button">Organization</button>
            <button class="tabs__button">Information</button>
            <button class="tabs__button">Contracts</button>
            <button class="tabs__button">Program</button>
            <button class="tabs__button">Settings</button>
            <button class="tabs__button">Help</button>
          </div>
        </div>

        <div class="header__donate">
          <img src="/img/testmenu/img/donates-ico.png" alt="" class="header__donate_decor">
          <div class="header__donate_text">
            <h3>Donate Coin</h3>
            <h4>13 337 666</h4>
          </div>
          <div class="header__donate_pluse">
            <img src="/testmenu/img/svg/pluse-ico.svg" alt="">
          </div>
        </div>
      </div>
    </header>

    <main class="main">
      <section class="stats">
        <div class="tabs__container container">
          <div class="stats__content">
            <div class="stats__items">
              <div class="stats__data">
                <img src="/img/testmenu/img/svg/phone-ico.svg" alt="" class="stats__data_ico"
                  style="width: 2.2604951560818085vh;height: 2.2604951560818085vh;margin-right: 1.6146393972012918vh;">

                <div class="stats__data_text">
                  <h3>Phone number</h3>
                  <h4>971 133 47 47</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/driving-ico.svg" alt="" class="stats__data_ico"
                  style="width: 3.013993541442411vh;height: 1.9375672766415502vh;margin-right: 1.1840688912809472vh;">

                <div class="stats__data_text">
                  <h3>Licenses</h3>
                  <h4>A / B / C / D / E</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/suitcase-ico.svg" alt="" class="stats__data_ico"
                  style="width: 2.798708288482239vh;height: 2.2604951560818085vh;margin-right: 1.2917115177610334vh;">

                <div class="stats__data_text">
                  <h3>Work</h3>
                  <h4>Loader</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/passport-ico.svg" alt="" class="stats__data_ico"
                  style="width: 2.4757804090419806vh;height: 3.2292787944025836vh;margin-right: 1.6146393972012918vh;">

                <div class="stats__data_text">
                  <h3>ID</h3>
                  <h4>1234</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/bank-ico.svg" alt="" class="stats__data_ico"
                  style="width: 3.1292787944025836vh;height: 3.1292787944025836vh;margin-right: 0.9687836383207751vh;">

                <div class="stats__data_text">
                  <h3>Bankkonto</h3>
                  <h4>971 133 47 47</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/rank-ico.svg" alt="" class="stats__data_ico"
                  style="width: 2.1528525296017222vh;height: 3.4445640473627557vh;margin-right: 2.1069967707212055vh;">

                <div class="stats__data_text">
                  <h3>Rang</h3>
                  <h4>1</h4>
                </div>
              </div>

              <div class="stats__data">
                <img src="/img/testmenu/img/svg/family-ico.svg" alt="" class="stats__data_ico"
                  style="width: 2.9063509149623252vh;height: 2.9063509149623252vh;margin-right: 1.2917115177610334vh;">

                <div class="stats__data_text">
                  <h3>Marital status</h3>
                  <h4>Verheiratet</h4>
                </div>
              </div>

              <div class="stats__premium">
                <h3>Premium</h3>
                <h4>ON</h4>
              </div>

              <div class="stats__wrong">
                <div class="stats__wrong_bans">
                  <h3>Ban</h3>
                  <h4>0</h4>
                </div>

                <div class="stats__wrong_warns">
                  <h3>Warnah</h3>
                  <h4>1</h4>
                </div>
              </div>
            </div>

            <div class="stats__nav nav">
              <div class="nav__categories">
                <div class="nav__categories_general">
                  <div class="nav__category nav__category--home">
                    <div class="nav__category__text">
                      <p class="nav__category_categories">Category</p>
                      <h3 class="nav__category_name">Haus</h3>
                      <h4 class="nav__category_status">Keins</h4>
                    </div>
                    <div class="nav__category__footer">
                      <a href="#">
                        <button class="btn btn--check">Look</button>
                      </a>
                    </div>
                  </div>

                  <div class="nav__category nav__category--biz">
                    <div class="nav__category__text">
                      <p class="nav__category_categories">Category</p>
                      <h3 class="nav__category_name">Business</h3>
                      <h4 class="nav__category_status">Absent</h4>
                    </div>
                    <div class="nav__category__footer">
                      <a href="#">
                        <button class="btn btn--check">Look</button>
                      </a>
                    </div>
                  </div>

                  <div class="nav__category nav__category--cars">
                    <div class="nav__category__text">
                      <p class="nav__category_categories">Category</p>
                      <h3 class="nav__category_name">Transport</h3>
                      <h4 class="nav__category_status">Absent</h4>
                    </div>
                    <div class="nav__category__footer">
                      <a href="#">
                        <button class="btn btn--check">Look</button>
                      </a>
                    </div>
                  </div>
                </div>
                <div class="nav__categories_select">
                  <div class="nav__reg">
                    <h3 class="nav__reg_category">
                      Category
                    </h3>
                    <h4 class="nav__reg_name">
                      Registration
                    </h4>
                    <p class="nav__reg_date">
                      29,06,2022 / 18:44
                    </p>
                  </div>

                  <div class="nav__report">
                    <div class="nav__report_text">
                      <h3 class="nav__report_category">
                        Category
                      </h3>
                      <h4 class="nav__report_name">
                        Report
                      </h4>
                      <p class="nav__report_descr">
                      Hier können Sie sich mit Anliegen und Fragen an die Serververwaltung wenden!
                      </p>
                    </div>
                    <div class="nav__report_button">
                      <a href="#">
                        <button class="btn btn--report">Kontaktieren Sie die Administration </button>
                      </a>
                    </div>
                  </div>
                </div>
              </div>
            </div>
          </div>
        </div>
      </section>
    </main>
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
html {
  box-sizing: border-box;
  overflow: hidden;
}

*,
*::before,
*::after {
  box-sizing: inherit;
  padding: 0;
  margin: 0;
  box-sizing: border-box;
  user-select: none;
  list-style: none;
  text-decoration: none;
}

h1,
h2,
h3,
h4,
h5,
h6,
p {
  margin: 0;
}

input,
textarea {
  outline: none;
}

input:active,
textarea:active {
  outline: none;
}

:focus {
  outline: none;
}

textarea {
  resize: none;
}

textarea {
  resize: vertical;
}

textarea {
  resize: horizontal;
}

a {
  text-decoration: none;
  color: inherit;
}

body {
  margin: 0;
  font-family: 'Akrobat';
  overflow-anchor: none;
  background-image: url('/img/testmenu/img/bg-home.jpg');
  background-size: cover;
  background-repeat: no-repeat;
  width: 100vw;
  height: 100vh;
  color: #fff;
}

ul {
  list-style-type: none;
}

.btn {
  font-family: 'Akrobat';
  cursor: pointer;
  padding: 0;
  background: transparent;
  border: none;
  color: #fff;
  transition: background-color 0.3s ease-in-out;

  &--check {
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
    padding: 7px 37.99999999999999px;
    font-weight: 600;
    font-size: 14px;
    line-height: 17px;
    text-transform: uppercase;
    transition: .3s all;

    &:hover {
      background: linear-gradient(180deg, #9b1017 0%, #4b0b0e 100%);
    }
  }

  &--report {
    background: linear-gradient(180deg, #301934  0%, #591b87 100%);
    box-shadow: inset 0px 0px 15px rgba(75, 0, 130, 0.86);
    padding: 7px 0;
    width: 100%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-weight: 600;
    font-size: 14px;
    line-height: 17px;
    text-transform: uppercase;
    transition: .3s all;

    &:hover {
      background: linear-gradient(180deg, #9b1017 0%, #4b0b0e 100%);
    }
  }
}

.container {
  max-width: 1725px;
  margin: 0 auto;
}

.tabs {
  color: #fff;

  &__list {
    display: grid;
    grid-auto-flow: column;
    grid-auto-columns: max-content;
    display: flex;
    align-items: flex-end;
    height: 121px;
    padding-bottom: 17px;
  }

  &__button {
    height: 100%;
    align-items: flex-end;
    display: flex;
    cursor: pointer;
    font-family: 'Akrobat';
    color: rgba(255, 255, 255, 0.25);
    padding: 0;
    text-transform: uppercase;
    border: none;
    background: transparent;
    font-weight: 700;
    font-size: 20px;
    line-height: 24px;
    transition: .3s all;

    &:not(:last-child) {
      margin-right: 40px;
    }

    &:hover {
      color: #ffffff;
    }

    &-active {
      color: #ffffff;
      position: relative;
      transition: .3s all;

      &::after {
        content: '';
        width: 100%;
        height: 5px;
        position: absolute;
        background: #5CFF80;
        left: 0;
        top: 0;
        border: 1px solid rgba(92, 255, 128, 0.09);
        box-shadow: 0 0 14px rgba(92, 255, 128, 0.55);
        transition: cubic-bezier(0.39, 0.575, 0.565, 1) .3s all;
      }
    }
  }
}

.header {
  margin-bottom: 50px;

  &__container {
    display: flex;
    justify-content: space-between;
    align-items: flex-end;
  }

  &__text {
    padding-top: 55px;
  }

  &__title {
    font-weight: 800;
    font-size: 40px;
    line-height: 48px;
    background: linear-gradient(89.71deg, #301934  0.25%, #591b87 99.8%);
    -webkit-background-clip: text;
    -webkit-text-fill-color: transparent;
    background-clip: text;
    text-fill-color: transparent;
    text-shadow: 0 0 113px rgba(255, 255, 255, 0.25);
    text-transform: uppercase;
  }

  &__descr {
    color: #fff;
    font-weight: 600;
    font-size: 15px;
    line-height: 18px;
    text-transform: uppercase;
  }

  &__donate {
    width: 204px;
    height: 71px;
    padding: 10px 16px 10px 9px;
    background: rgba(255, 255, 255, 0.05);
    border-radius: 44px;
    display: flex;
    align-items: center;

    &_decor {
      width: 50px;
      height: 50px;
      object-fit: cover;
      margin-right: 14px;
    }

    &_text {
      margin-right: 16px;

      h3 {
        font-weight: 700;
        font-size: 14px;
        line-height: 17px;
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.25);
      }

      h4 {
        font-weight: 700;
        font-size: 20px;
        line-height: 24px;
        text-transform: uppercase;
        color: #5CFF80;
      }
    }

    &_pluse {
      cursor: pointer;
      background: linear-gradient(180deg, #FFCD4D 0%, #FFAD4D 100%);
      box-shadow: 0px 4px 17px rgba(255, 201, 77, 0.55);
      border-radius: 100%;
      width: 25px;
      height: 25px;
      display: flex;
      align-items: center;
      justify-content: center;
      vertical-align: middle;
    }
  }
}

.stats {

  &__content {
    display: flex;
    gap: 93.55px;
    padding-left: 5%;
  }

  &__data {
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1 round round;
    border-image-repeat: unset;
    display: flex;
    align-items: center;
    background-image: url('/img/testmenu/img/stats/bg-data.png');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    width: 360px;
    height: 72px;
    padding: 20px 0 20px 30px;
    position: relative;

    &::after {
      content: '';
      position: absolute;
      left: 0;
      top: 50%;
      background: #FFFFFF;
      border: 1px solid rgba(255, 255, 255, 0.09);
      box-shadow: 0 0 11.73950484391819px rgba(255, 255, 255, 0.55);
      transform: rotate(-90deg);
      width: 21.801937567276642px;
      height: 1.6770721205597416px;
    }

    &_text {
      h3 {
        font-weight: 700;
        font-size: 10.06243272335845px;
        line-height: 11.73950484391819px;
        text-transform: uppercase;
        color: rgba(255, 255, 255, 0.55);
      }

      h4 {
        font-weight: 700;
        font-size: 13.416576964477933px;
        line-height: 15.932185145317543px;
        text-transform: uppercase;
      }
    }
  }

  &__premium {
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1 round round;
    border-image-repeat: unset;
    display: flex;
    background-image: url('/img/testmenu/img/stats/stats-premiums.png');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    width: 301.87298170075354px;
    height: 75.46824542518839px;
    padding: 24.317545748116252px 0 24.317545748116252px 55.343379978471475px;
    flex-direction: column;

    h3 {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    h4 {
      font-weight: 700;
      font-size: 13.416576964477933px;
      line-height: 15.932185145317543px;
      text-transform: uppercase;
    }
  }

  &__wrong {
    border: 1px solid;
    border-image: linear-gradient(90deg, rgba(255, 255, 255, 0.09) 0%, rgba(255, 255, 255, 0) 101.25%) 1 round round;
    border-image-repeat: unset;
    display: flex;
    background-image: url('/img/testmenu/img/stats/wrongs-stats.png');
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    width: 301.87298170075354px;
    height: 75.46824542518839px;
    padding: 24.317545748116252px 0 24.317545748116252px 55.343379978471475px;

    &_bans {
      margin-right: 26.833153928955866px;
    }

    h3 {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    h4 {
      font-weight: 700;
      font-size: 13.416576964477933px;
      line-height: 15.932185145317543px;
      text-transform: uppercase;
    }
  }

  &__items {
    display: flex;
    flex-direction: column;
    gap: 8.385360602798709px;
  }
}

.nav {

  &__categories {
    display: flex;

    &_select {
      display: flex;
      gap: 20px;
    }

    &_general {
      display: flex;
      flex-direction: column;
      gap: 12.578040904198062px;
      max-width: 361.87298170075354px;
      margin-right: 33.541442411194836px;
    }
  }

  &__reg {
    background-image: url('/img/testmenu/img/stats/bg-reg.png');
    background-repeat: no-repeat;
    background-size: contain;
    width: 407px;
    height: 100%;
    padding: 16.770721205597418px 0 0 35.21851453175458px;
    transition: .7s all;

    &:hover {
      cursor: pointer;
      transform: scale(1.03);
    }

    &_category {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    &_name {
      font-weight: 700;
      font-size: 40.2497308934338px;
      line-height: 48.635091496232505px;
      text-transform: uppercase;
      color: #FFFFFF;
    }

    &_date {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }
  }

  &__report {
    background-image: url('/img/testmenu/img/stats/bg-report.png');
    background-repeat: no-repeat;
    background-size: contain;
    width: 361px;
    height: 100%;
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    transition: .7s all;

    &:hover {
      cursor: pointer;
      transform: scale(1.03);
    }

    &_text {
      padding: 16.770721205597418px 0 0 46.18908180839613px;
    }

    &_category {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    &_name {
      font-weight: 700;
      font-size: 40.2497308934338px;
      line-height: 48.635091496232505px;
      text-transform: uppercase;
      color: #FFFFFF;
    }

    &_descr {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
      max-width: 192.02475780409043px;
    }

    &_button {
      margin-top: auto;
      padding: 12.578040904198062px;
    }
  }

  &__category {
    background-repeat: no-repeat;
    background-size: cover;
    background-position: center center;
    width: 361.7115177610334px;
    height: 208.41550053821314px;
    transition: .7s all;

    &:hover {
      cursor: pointer;
      transform: scale(1.03);
    }

    &--home {
      background-image: url('/img/testmenu/img/stats/house-category.png');
    }

    &--biz {
      background-image: url('/img/testmenu/img/stats/biz-category.png');
    }

    &--cars {
      background-image: url('/img/testmenu/img/stats/cars-category.png');
    }

    &__text {
      padding: 15.932185145317543px 0 12.578040904198062px 25.156081808396124px;
      margin-bottom: 58px;
    }

    &_categories {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    &_name {
      font-weight: 700;
      font-size: 40.2497308934338px;
      line-height: 48.635091496232505px;
      text-transform: uppercase;
      color: #FFFFFF;
    }

    &_status {
      font-weight: 700;
      font-size: 10.06243272335845px;
      line-height: 11.73950484391819px;
      text-transform: uppercase;
      color: rgba(255, 255, 255, 0.55);
    }

    &__footer {
      padding-left: 12.578040904198062px;
    }
  }
}

@media (max-width: 2000px) and (min-height: 1080px) {
  .header .header__container {
    padding: 0 5%;
  }
}

@media (min-width: 2500px) {
  .stats .stats__content {
    padding-left: 0;
  }
}

@media (max-width: 2200px) and (min-height: 1079px) {
  .stats .stats__content {
    gap: 3.8740371099999997px;
  }
}

@media (max-width: 1000px) {
  .stats .stats__content {
    gap: 3.8740371099999997px;
  }
}

/*@media (max-width: 1630px) {
  .tabs .tabs__button {
    font-size: 17px;
    line-height: 17px;
  }

  .header .header__title {
    font-size: 30px;
    line-height: 30px;
  }

  .header .header__descr {
    font-size: 11px;
    line-height: 13px;
  }
}*/
</style>
