<template>
  <div class="body-tab community-tab">
    <div class="body-tab__title">Family registration</div>
    <div class="body-tab__desc">In this section you can create your own community (family)</div>
    <div class="page">
      <div class="page__founders">
        <div class="title">Family owner</div>
        <div class="desc">The name of the founder</div>
        <div class="owner">{{ name }}</div>
        <!-- <div class="prompt">{{ loc("cityHallWeb_2") }}</div> -->
        <div class="founders-list scroll">
          <div
            class="founders-list__item"
            v-for="item in foundersList"
            :key="item.id"
          >
            <input
              type="text"
              v-model="item.value"
              :placeholder="loc('chall:commun:6')"
            />
          </div>
        </div>
        <div class="title-wrap" @click="addFounder">
          <img src="/img/cityHallWeb/community/+.svg" alt="" />
          <div class="title-wrap__value">Add the founder</div>
          <!-- <div class="title-wrap__btn btn" >
            <div class="text" v-if="foundersList.length === 0">
              {{ loc("cityHallWeb_4") }}
            </div>
          </div> -->
        </div>
      </div>
      <!-- <div class="prompt-wrap">
        <div class="prompt-wrap__desc">{{ loc("cityHallWeb_5") }}</div>
        <div class="prompt-wrap__prompt">{{ loc("cityHallWeb_6") }}</div>
      </div> -->
      <div class="page__info">
        <div class="info-wrap scroll">
          <div class="title">Change the name</div>
          <div class="page__info_wrap">
            <div class="page__info-list">
              <div>
                <div class="page__desc">{{ loc("cityHallWeb_7") }}</div>
                <menu-drop
                  :dropList="list"
                  :currentItem="currentCommunity"
                  :setCurrentItem="setCurrentItem"
                />
              </div>
              <div>
                <div class="page__desc">{{ loc("cityHallWeb_8") }}</div>
                <input
                  :placeholder="loc('chall:commun:7')"
                  v-model="currentName"
                />
              </div>
              <div>
                <div class="page__desc">{{ loc("cityHallWeb_9") }}</div>
                <input
                  :placeholder="loc('chall:commun:7')"
                  v-model="currentNation"
                />
              </div>
            </div>

            <div>
              <div class="page__desc">{{ loc("cityHallWeb_10") }}</div>
              <textarea
                :placeholder="loc('chall:commun:7')"
                v-model="currentReason"
              ></textarea>
              <div
                class="hide"
                v-if="
                  currentCommunity &&
                  currentName &&
                  currentNation &&
                  currentReason
                "
              >
                <div class="price">
                  <div>The costs of creation</div>
                  <div>${{ currentTax }}</div>
                </div>
                <div>
                  <div class="community-btn bank-btn" @click="sendForm">
                    <span>Pay</span>
                  </div>
                  <Payment
                    :currentPayment="currentPayment"
                    :setCurrentPayment="setCurrentPayment"
                  />
                </div>
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import MenuDrop from "../components/MenuDrop";
import Payment from "../components/Payment";
import { mapGetters, mapState } from "vuex";
export default {
  name: "CommunityTab",

  components: {
    MenuDrop,
    Payment,
  },

  data: function () {
    return {
      foundersList: [],
      currentCommunity: null,
      currentName: null,
      currentNation: null,
      currentReason: null,
      currentTax: null,
      currentPayment: 0, //  0 - money, 1 - card
    };
  },

  methods: {
    addFounder: function () {
      this.foundersList.push({ value: "" });
    },

    setCurrentItem: function (obj) {
      this.currentCommunity = obj;
    },

    setCurrentTax: function (value) {
      this.currentTax = value;
    },

    setCurrentPayment: function (value) {
      this.currentPayment = value;
    },

    sendForm: function () {
      window.mp.trigger(
        "cityHallWeb:sendFormTaxPage",
        JSON.stringify(this.foundersList.map((item) => item.value)),
        this.currentCommunity,
        this.currentName,
        this.currentNation,
        this.currentReason,
        this.currentPayment
      );
      this.foundersList = [];
      this.currentCommunity = null;
      this.currentName = null;
      this.currentNation = null;
      this.currentReason = null;
    },
  },

  computed: {
    ...mapState("cityHallWeb", ["name", "cardId"]),
    ...mapState("cityHallWeb/community", ["list", "tax"]),
    ...mapGetters("localization", ["loc"]),
  },

  mounted() {
    this.currentCommunity = this.list[0];
    this.setCurrentTax(this.tax);
  },
};
</script>

<style lang="scss" scoped>
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.city-hall-web {
  .body-tab {
    &__title {
      margin-bottom: conv(7);
    }

    &__desc {
      margin-bottom: conv(45);
    }
  }

  .community-tab {
    display: flex;
    flex-flow: column;

    .title {
      font-weight: 800;
      font-size: conv(36);
      line-height: conv(43);
      text-transform: uppercase;
      color: #ffffff;
      margin-bottom: conv(35);
    }

    input,
    textarea {
      width: 100%;
    }

    textarea {
      resize: none;
      height: conv(246);
      background: rgba(0, 0, 0, 0.2);
      padding: conv(20);
      color: white;
      border: none;
      outline: none;

      &,
      &::placeholder {
        font-weight: 700;
        font-size: conv(20);
        line-height: conv(24);
        display: flex;
        align-items: center;
        text-transform: uppercase;
      }

      &::placeholder {
        color: rgba(255, 255, 255, 0.2);
      }
    }

    .scroll {
      &::-webkit-scrollbar {
        width: 0.3rem;
        background-color: transparent;
        &-thumb {
          background: rgba(70, 85, 95, 0.1);
          border-radius: 0.1rem;
        }
      }
    }

    .page {
      width: 100%;
      height: conv(624);
      display: grid;
      grid-template-columns: conv(478) 1fr;
      column-gap: conv(20);

      &__founders {
        width: 100%;
        height: 100%;
        background: rgba(255, 255, 255, 0.03);
        display: flex;
        flex-direction: column;
        padding: conv(33) 0 conv(30) conv(38);

        .desc {
          font-weight: 600;
          font-size: conv(20);
          line-height: conv(24);
          text-transform: uppercase;
          color: rgba(255, 255, 255, 0.5);
          margin-bottom: conv(3);
        }

        .owner {
          font-weight: 700;
          font-size: conv(32);
          line-height: conv(38);
          text-transform: uppercase;
          color: #ffffff;
          margin-bottom: conv(32);
        }

        .title-wrap {
          width: conv(375);
          height: conv(89);
          display: flex;
          justify-content: center;
          align-items: center;
          cursor: pointer;
          background: rgba(255, 255, 255, 0.07);
          transition: 0.2s ease;

          &:hover {
            background: rgba(255, 255, 255, 0.15);
          }

          img {
            width: conv(32);
            height: conv(32);
            margin-right: conv(22);
          }

          &__value {
            font-weight: 500;
            font-size: conv(24);
            line-height: conv(29);
            text-transform: uppercase;
            color: #ffffff;
          }
        }

        .founders-list {
          display: flex;
          flex-flow: column;
          width: conv(390);
          padding-right: conv(15);
          overflow-y: auto;
          overflow-x: hidden;
          height: conv(287);
          max-height: conv(287);
          margin-bottom: conv(10);

          &::-webkit-scrollbar {
            background: rgba(255, 255, 255, 0.05);
            width: conv(5);

            &-thumb {
              background: #301934 ;
            }
          }

          &__item {
            width: conv(375);
            height: conv(89);
            min-height: conv(89);
            background: rgba(0, 0, 0, 0.2);

            &:not(:last-child) {
              margin-bottom: conv(10);
            }

            input {
              width: 100%;
              margin: 0;
              height: 100%;
              color: white;
              background: none;

              &,
              &::placeholder {
                font-weight: 700;
                font-size: conv(20);
                line-height: conv(24);
                text-transform: uppercase;
              }

              &::placeholder {
                color: rgba(255, 255, 255, 0.2);
              }
            }
          }
        }
      }
      /* .prompt-wrap {
        &__desc {
          font-family: Merriweather;
          font-style: normal;
          font-weight: bold;
          font-size: 2rem;
          line-height: 2.5rem;
          color: #46555f;
          margin-bottom: 0.5rem;
        }
        &__prompt {
          font-family: Roboto;
          font-style: normal;
          font-weight: normal;
          font-size: 0.8rem;
          line-height: 0.95rem;
          color: #46555f;
        }
        margin-right: 3rem;
      } */
      &__info {
        width: 100%;
        height: 100%;
        display: flex;
        flex-direction: column;
        background: rgba(255, 255, 255, 0.03);
        padding: conv(33) conv(38) 0;

        .page__desc {
          padding-left: conv(20);
          font-weight: 700;
          font-size: conv(20);
          line-height: conv(42);
          text-transform: uppercase;
          color: #ffffff;
          margin: 0;

          &::before {
            display: none;
          }
        }

        &_wrap {
          display: grid;
          grid-template-columns: conv(342) 1fr;
          column-gap: conv(40);
        }

        &-list {
          display: flex;
          flex-direction: column;

          & > div:not(:last-child) {
            margin-bottom: conv(26);
          }

          input {
            width: 100%;
            height: conv(89);
            min-height: conv(89);
            background: rgba(0, 0, 0, 0.2);
            border: none;
            outline: none;
            padding-left: conv(20);
            color: white;
            margin: 0;

            &,
            &::placeholder {
              font-weight: 700;
              font-size: conv(20);
              line-height: conv(24);
              text-transform: uppercase;
            }

            &::placeholder {
              color: rgba(255, 255, 255, 0.2);
            }
          }
        }

        .info-wrap {
          display: flex;
          flex-direction: column;
        }
        .page__btn {
          span {
            &:nth-child(1) {
              margin-right: 0.6rem;
              padding-right: 0.65rem;
              position: relative;
              display: flex;
              align-items: center;
              &:after {
                content: "";
                width: 1px;
                height: 1.5rem;
                background-color: rgba(255, 255, 255, 0.3);
                position: absolute;
                right: 0;
              }
            }
          }
        }
      }
    }

    .hide {
      margin-top: conv(19);
      width: conv(610);

      .price {
        display: flex;
        align-items: center;
        margin-bottom: conv(25);

        div {
          &:first-child {
            font-weight: 700;
            font-size: conv(16);
            line-height: conv(19);
            text-transform: uppercase;
            color: #ffffff;
            margin-right: conv(19);
          }

          &:last-child {
            font-weight: 700;
            font-size: conv(32);
            line-height: conv(38);
            text-transform: uppercase;
            color: #a0ff98;
          }
        }
      }

      & > div:last-child {
        display: flex;
        align-items: center;
        justify-content: space-between;
      }

      .community-btn {
        width: conv(295);
        min-width: conv(295);
        height: conv(75);
        cursor: pointer;
        display: flex;
        align-items: center;
        justify-content: center;
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        text-align: center;
        text-transform: uppercase;
        color: #ffffff;
        // background: linear-gradient(
        //   180deg,
        //   #301934  0%,
        //   #591b87 100%,
        //   #591b87 100%
        // );
      }
    }
  }
}
</style>
