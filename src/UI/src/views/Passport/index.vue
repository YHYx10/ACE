<template>
  <div class="passport">
    <div class="passport-wrap">
      <div class="passport-top">
        <div class="passport-top_title">We the people</div>
        <div class="passport-top_sign">
          <div>
            {{ firstname[0] }}.
            {{ lastname[0].toUpperCase() + lastname.slice(1) }}
          </div>
          <div></div>
          <span>The signature of the carrier</span>
        </div>
      </div>
      <div class="passport-line"></div>
      <div class="passport-bottom">
        <div class="passport-bottom_info">
          <div class="passport-bottom_img">
            <span>PASSPORT</span>
            <div>
              <img src="img/passport/main_avatar.png" alt="" />
            </div>
          </div>

          <div class="passport-bottom_information">
            <div class="passport-bottom_title">
              <span>SAN ANDREAS</span>
              <span>Astro RolePlay</span>
            </div>

            <div class="passport-bottom_wrap">
              <div>
                <div
                  class="passport-bottom_item"
                  v-for="(elem, index) in [
                    firstname,
                    lastname,
                    work,
                    member,
                    gender,
                  ]"
                  :key="index"
                >
                  <span>{{ firstInfo[index] }}</span>
                  <span>{{ elem }}</span>
                </div>
              </div>
              <div>
                <div
                  class="passport-bottom_item"
                  v-for="(elem, index) in [date, partner, number]"
                  :key="index"
                >
                  <span>{{ secondInfo[index] }}</span>
                  <span>{{ elem }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>
        <div class="passport-bottom_series">
          LS/{{ lastname }}/{{ firstname }}/546784/{{ number }}
        </div>
        <!--  LS&#60;{{ lastname }}&#60;{{ firstname }}&#60;&#60;546784/{{ number }} -->
      </div>
    </div>
    <div class="passport-esc">
      <div>Esc</div>
    Close
    </div>
    <!-- <span @click="exit" class="passport__close">&times;</span>
    <div class="passport__top">
      <span class="top__seria">{{ number }}</span>
      <div class="top__general">
        <img
          src="img/passport/seal_top.png"
          alt="seal"
          class="general__img"
        >
        <div class="general__state">
          <span class="state__title">State of</span>
          <span class="state__value">San andreas</span>
        </div>
        <div class="general__issue">
          <p class="issue__title">Date of issue</p>
          <span class="issue__date">{{ date }}</span>
        </div>
      </div>
    </div>
    <div class="passport__bottom">
      <div class="bottom__header">
        <div class="header__number">
          <span class="number__title">PASSPORT №</span>
          <span class="number__value">{{ number }}</span>
        </div>
        <span class="header__city">CITY OF LOS SANTOS</span>
        <div class="header__seria">
          <span class="seria__title">SERIES</span>
          <span class="seria__value">546784</span>
        </div>
      </div>
      <div class="bottom__main">
        <img
          src="img/passport/main_avatar.png"
          alt="avatar"
          class="main__avatar"
        >
        <div class="main__general-info">
          <div class="general-info__item surename">
            <p class="item__title">surename</p>
            <span class="item__value">{{ lastname }}</span>
          </div>
          <div class="general-info__item given-name">
            <p class="item__title">given name</p>
            <span class="item__value">{{ firstname }}</span>
          </div>
          <div class="general-info__item job">
            <p class="item__title">job</p>
            <span class="item__value">{{ loc(work) }}</span>
          </div>
          <div class="general-info__item employee">
            <p class="item__title">member</p>
            <span class="item__value">{{ loc(member) }}</span>
          </div>
          <div class="general-info__item sex">
            <p class="item__title">sex</p>
            <span class="item__value">{{ loc(gender) }}</span>
          </div>
        </div>
        <div class="main__other-info">
          <div class="other-info__item sex">
            <p class="item__title">Date of registration</p>
            <span class="item__value">{{ date }}</span>
          </div>
          <div class="other-info__item merry">
            <p class="item__title">Merried to</p>
            <span class="item__value">{{ loc(partner) }}</span>
          </div>
          <div class="other-info__images">
            <img
              src="img/passport/seal_bottom_first.png"
              alt="seal"
              class="item__img first"
            >
            <img 
              src="img/passport/seal_bottom_second.png"
              alt="seal"
              class="item__img second"
            >
          </div>
        </div>
      </div>
    </div>
    <div class="passport__footer">
      <span class="footer__info">LS/{{ lastname }}/{{ firstname }}/546784/{{ number }}</span>
    </div> -->
  </div>
</template>

<script>
import { mapState, mapGetters } from "vuex";

export default {
  name: "Passport",

  data() {
    return {
      firstInfo: ["Name", "surname", "Work "," Position ", "Gender"],
      secondInfo: ["Registration date "," wife "," passport number"],
    };
  },

  computed: {
    ...mapState("passport", [
      "number",
      "firstname",
      "lastname",
      "date",
      "gender",
      "member",
      "work",
      "partner",
    ]),
    ...mapGetters("localization", ["loc"]),
  },

  methods: {
    exit: function () {
      window.mp.trigger("passport:close");
    },
  },
};
</script>

<style lang="scss" scoped>
$width: 1920;
$height: 1080;

@function conv($px, $direction: false) {
  @if $direction {
    @return ($px / $height) * 100vh;
  } @else {
    @return ($px / $width) * 100vw;
  }
}

.passport {
  position: absolute;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  display: flex;
  flex-direction: column;
  align-items: center;
  font-family: "Akrobat";
  padding-top: conv(172, true);

  &-wrap {
    display: grid;
    grid-template-rows: 1fr auto 1fr;
    background: #ffffff;
    border-radius: conv(10);
    position: relative;
    overflow: hidden;

    div,
    img,
    span {
      z-index: 2;
    }

    &::after {
      content: "";
      position: absolute;
      right: 0;
      z-index: 1;
      opacity: 0.51;
      top: 0;
      width: conv(765, true);
      height: conv(407, true);
      background: url(/img/passport/bg-eagle.png) left top no-repeat;
      background-size: contain;
      transform: matrix(-1, 0, 0, 1, 0, 0);
    }
  }

  &-top {
    display: flex;
    flex-direction: column;
    justify-content: space-between;
    align-items: flex-start;
    padding: conv(13, true) 0 conv(21, true) conv(28, true);

    &_title {
      font-family: "Bokor", cursive;
      font-weight: 400;
      font-size: conv(48, true);
      line-height: conv(87, true);
      text-transform: capitalize;
      color: #67635e;
    }

    &_sign {
      position: relative;
      padding-left: conv(9);
      display: flex;
      flex-direction: column;
      align-items: center;

      div:first-child {
        text-align: center;
        font-size: conv(30, true);
        line-height: 100%;
        margin-bottom: conv(-3, true);
        color: #424153;
        font-family: 'Sacramento', cursive;
      }

      div:nth-child(2) {
        width: conv(227);
        height: conv(2, true);
        background: #424153;
        margin-bottom: conv(3, true);
      }

      span {
        display: block;
        font-weight: 700;
        font-size: conv(10, true);
        line-height: conv(12, true);
        text-transform: uppercase;
        color: #424153;
      }
    }
  }

  &-line {
    width: 100%;
    height: conv(2, true);
    border: 1px solid rgba(0, 0, 0, 0.14);
    filter: drop-shadow(0px 0px conv(7, true) rgba(0, 0, 0, 0.4));
  }

  &-bottom {
    padding: conv(43, true) conv(54) conv(13, true) conv(54);
    display: grid;
    grid-template-rows: 1fr auto;
    position: relative;

    div,
    img,
    span {
      z-index: 2;
    }

    &::after {
      content: "";
      position: absolute;
      right: conv(45);
      bottom: conv(34, true);
      width: conv(234, true);
      height: conv(234, true);
      background: url(/img/passport/check.png) center center no-repeat;
      background-size: cover;
      z-index: 1;
    }

    &_info {
      display: grid;
      grid-template-columns: conv(142) 1fr;
      column-gap: conv(20);
    }

    &_img {
      padding-top: conv(20, true);
      display: flex;
      flex-direction: column;
      align-items: center;

      span {
        display: block;
        margin-bottom: conv(14, true);
        font-weight: 700;
        font-size: conv(20, true);
        line-height: conv(24, true);
        text-transform: uppercase;
        color: #424153;
      }

      div {
        width: 100%;
        height: conv(166, true);
        background: #eeeeee;

        img {
          width: 100%;
          height: 100%;
        }
      }
    }

    &_information {
      width: 100%;
      height: 100%;
      margin-bottom: conv(30, true); /* test */
    }

    &_title {
      display: flex;
      align-items: center;
      margin-bottom: conv(20, true);

      span {
        display: block;
        font-weight: 700;
        font-size: conv(32, true);
        line-height: conv(38, true);
        text-transform: uppercase;
        color: #67635e;

        &:not(:last-child) {
          margin-right: conv(36);
        }
      }
    }

    &_wrap {
      display: grid;
      grid-template-columns: 1fr 1fr;

      & > div {
        display: flex;
        flex-direction: column;

        &:last-child {
          margin-left: conv(30);
        }
      }
    }

    &_item {
      &:not(:last-child) {
        margin-bottom: conv(3, true);
      }

      span {
        display: block;
        text-transform: uppercase;
        font-weight: 700;
        color: #424153;

        &:first-child {
          font-size: conv(10, true);
          line-height: conv(12, true);
        }

        &:last-child {
          font-size: conv(20, true);
          line-height: conv(24, true);
        }
      }
    }

    &_series {
      text-align: center;
      width: 100%;
      white-space: nowrap;
      font-weight: 700;
      font-size: conv(16, true);
      line-height: con(19, true);
      letter-spacing: 0.2em;
      text-transform: uppercase;
      color: rgba(66, 65, 83, 0.5);
    }
  }

  &-esc {
    margin-top: conv(40, true);
    display: flex;
    align-items: center;
    font-weight: 700;
    font-size: conv(18, true);
    line-height: conv(22, true);
    letter-spacing: 0.03em;
    color: #ffffff;

    div {
      display: flex;
      justify-content: center;
      align-items: center;
      background: rgba(0, 0, 0, 0.4);
      border: 1px solid rgba(255, 255, 255, 0.07);
      /* backdrop-filter: blur(3px); */
      border-radius: conv(3);
      width: conv(50, true);
      height: conv(50, true);
      font-weight: 700;
      font-size: conv(20, true);
      line-height: conv(24, true);
      text-align: center;
      color: #ffffff;
      margin-right: conv(10);
    }
  }
}
/* .passport {
  background-image: url('../../../public/img/passport/main_bg.png');
  background-size: cover;
  width: 436px;
  height: 600px;
  padding: 10px 46px;
  color: #7F6F6F;
  position: relative;
  &__close {
    position: absolute;
    top: 20px;
    right: 20px;
    background-color: rgb(134, 56, 56);
    width: 24px;
    height: 24px;
    border-radius: 50%;
    display: flex;
    align-items: center;
    justify-content: center;
    font-size: 24px;
    color: #fff;
  }
  &__top {
    display: flex;
    flex-direction: column;
    align-items: center;
    .top__seria {
      font-weight: bold;
      letter-spacing: 5px;
    }
    .top__general {
      display: flex;
      flex-direction: column;
      align-self: flex-start;
      margin: 20px 0 0 0;
      .general__img {
        align-self: flex-start;
      }
      .general__state {
        margin: 5px 0 0 0;
        display: flex;
        flex-direction: column;
        text-transform: uppercase;
        font-weight: bold;
      }
      .general__issue {
        margin: 45px 0 0 0;
        text-transform: uppercase;
        .issue__title {
          font-size: 9px;
        }
        .issue__date {
          font-weight: bold;
        }
      }
    }
  }
  &__bottom {
    margin: 72px 0 0 0;
    .bottom__header {
      display: flex;
      align-items: center;
      justify-content: space-between;
      .header__number {
        display: flex;
        .number__title {
          font-size: 8px;
          text-transform: uppercase;
        }
        .number__value {
          font-size: 8px;
          font-weight: bold;
          margin: 0 0 0 3px;
        }
      }
      .header__city {
        text-transform: uppercase;
        font-weight: bold;
      }
      .header__seria {
        display: flex;
        .seria__title {
          font-size: 8px;
          text-transform: uppercase;
        }
        .seria__value {
          font-size: 8px;
          font-weight: bold;
          margin: 0 0 0 3px;
        }
      }
    }
    .bottom__main {
      margin: 12px 0 0 0;
      display: flex;
      .main__avatar {
        align-self: flex-start;
      }
      .main__general-info {
        margin: 0 0 0 10px;
        height: 200px;
        width: 220px;
        display: flex;
        flex-direction: column;
        .general-info__item {
          display: flex;
          flex-direction: column;
          margin: 8px 0 0 0;
          &:first-child {
            margin: 0;
          }
          .item__title {
            font-size: 8px;
            text-transform: uppercase;
          }
        }
      }
      .main__other-info {
        display: flex;
        flex-direction: column;
        .other-info__item {
          display: flex;
          flex-direction: column;
          &.merry {
            margin: 8px 0 0 0;
          }
          .item__title {
            font-size: 8px;
            text-transform: uppercase;
          }
        }
        .other-info__images {
          align-self: flex-end;
          position: relative;
          width: 135px;
          height: 135px;
          .item__img {
            transform: translate(-50%, -50%);
            position: absolute;
            top: 50%;
            left: 50%;
          }
        }
      }
    }
  }
  &__footer {
    text-align: center;
    margin: 20px 0 0 0;
    .footer__info {
      font-size: 11px;
    }
  }
} */
</style>
