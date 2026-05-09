<template>
  <div class="autoschool-general">
    <div class="autoschool-general_left">
      <div class="autoschool-title">
        <span>Center</span>
       Licensing
      </div>
      <div class="autoschool-descr">
        {{ getType() }}
      </div>

      <img
        :src="`/img/autoSchool/general/${typeExam}.png`"
        :class="{ airplane: typeExam === 1 }"
        alt=""
      /><!-- dinaic -->
    </div>

    <div class="autoschool-general_right">
      <div class="autoschool-general_title">Examination Selection</div>
      <div class="autoschool-general_right-list">
        <div class="autoschool-general_item">
          <div class="autoschool-general_item-title">Theoretically test</div>
          <div class="autoschool-general_item-img">
            <img src="/img/autoSchool/general/theory.png" alt="" />
          </div>
          <div class="autoschool-general_item-footer">
            <div class="autoschool-general_item-price">
              <span>Freely</span>
              <svg
                width="29"
                height="32"
                viewBox="0 0 29 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M15.6417 8.8324L15.3689 8.80225C15.2284 10.0817 14.7528 11.3098 13.9928 12.3522C13.9907 12.3556 13.988 12.359 13.9859 12.3625C14.0654 12.4077 14.1415 12.4584 14.2141 12.5146C14.2868 12.5708 14.356 12.6311 14.4204 12.6962C14.4252 12.6894 14.4307 12.6825 14.4355 12.6756C14.4389 12.6722 14.4417 12.6681 14.4437 12.664C15.2551 11.5462 15.7636 10.2318 15.9144 8.86255L15.6417 8.8324Z"
                  fill="#FFC035"
                />
                <path
                  d="M28.0027 23.3713L18.6056 30.8913C17.8305 31.5108 16.7004 31.384 16.0816 30.611L6.7627 18.9647L7.10124 15.9144L7.16155 15.3689L7.51586 12.1726L7.9805 7.9812L12.1726 8.44653L15.3689 8.80221C15.2284 10.0817 14.7528 11.3098 13.9928 12.3521C13.9907 12.3556 13.988 12.359 13.9859 12.3624C13.6775 12.1822 13.3184 12.0794 12.936 12.0794C11.7792 12.0794 10.841 13.0169 10.841 14.1737C10.841 14.393 10.8746 14.6048 10.9369 14.8028C10.9644 14.8905 10.9979 14.9762 11.0363 15.0584C11.0754 15.1421 11.1199 15.2222 11.1693 15.2997C11.5407 15.8822 12.1931 16.2687 12.936 16.2687C14.0928 16.2687 15.0303 15.3305 15.0303 14.1737C15.0303 13.5967 14.7973 13.0745 14.4204 12.6962C14.4252 12.6893 14.4307 12.6825 14.4355 12.6756C14.4389 12.6722 14.4416 12.6681 14.4437 12.664C15.2551 11.5462 15.7636 10.2318 15.9144 8.86252L18.964 9.20175L20.7459 11.4283L28.283 20.848C28.9018 21.621 28.7777 22.7518 28.0027 23.3713Z"
                  fill="#FFC035"
                />
                <g opacity="0.2">
                  <path
                    d="M28.4625 22.821C28.3515 23.0273 28.1973 23.2158 28.0027 23.3713L18.6056 30.8913C17.8305 31.5108 16.7004 31.384 16.0816 30.611L6.7627 18.9647L7.10124 15.9144L7.16155 15.3689L7.51586 12.1726L7.9805 7.9812L8.783 8.07029L8.42938 11.2591L8.07507 14.4554L8.01476 15.0009L7.67622 18.0512L16.9951 29.6975C17.614 30.4705 18.7441 30.5973 19.5191 29.9778L28.4625 22.821Z"
                    fill="#303C44"
                  />
                </g>
                <path
                  d="M15.9624 7.98118C15.9624 8.27518 15.9459 8.57192 15.9144 8.86249C15.7636 10.2318 15.2551 11.5462 14.4437 12.6639C14.4416 12.668 14.4389 12.6722 14.4355 12.6756C14.4307 12.6824 14.4252 12.6893 14.4204 12.6961C13.5843 13.8372 12.4611 14.737 11.1693 15.2997C11.1199 15.2222 11.0754 15.142 11.0363 15.0584C10.9979 14.9762 10.9644 14.8905 10.9369 14.8028C12.1486 14.2779 13.2019 13.4349 13.9845 12.3645C13.9852 12.3638 13.9852 12.3631 13.9859 12.3624C13.988 12.359 13.9907 12.3555 13.9928 12.3521C14.7528 11.3098 15.2284 10.0817 15.3689 8.80219C15.399 8.53149 15.4141 8.2553 15.4141 7.98118C15.4141 3.88231 12.08 0.548252 7.98118 0.548252C3.88299 0.548252 0.548252 3.88231 0.548252 7.98118C0.548252 11.7799 3.39094 14.9556 7.16154 15.3689L7.10123 15.9144C3.05308 15.4703 0 12.0602 0 7.98118C0 3.58009 3.58009 0 7.98118 0C12.3823 0 15.9624 3.58009 15.9624 7.98118Z"
                  fill="#303C44"
                />
              </svg>
            </div>
            <button
              class="bank-btn autoschool-general_item-btn"
              @click="startExam('theory')"
            >
       Start the exam
            </button>
          </div>
        </div>
        <div
          class="autoschool-general_item"
          :class="{ lock: !theoryList[typeExam] }"
        >
          <div class="autoschool-general_item-title"> Practical examination</div>
          <div class="autoschool-general_item-img">
            <img
              :src="`/img/autoSchool/general/practic-${typeExam}.png`"
              alt=""
            />
          </div>
          <div class="autoschool-general_item-footer">
            <div class="autoschool-general_item-price">
              <span>The costs of the examination:${{ practicePriceList[typeExam] }}</span>
              <svg
                width="29"
                height="32"
                viewBox="0 0 29 32"
                fill="none"
                xmlns="http://www.w3.org/2000/svg"
              >
                <path
                  d="M15.6417 8.8324L15.3689 8.80225C15.2284 10.0817 14.7528 11.3098 13.9928 12.3522C13.9907 12.3556 13.988 12.359 13.9859 12.3625C14.0654 12.4077 14.1415 12.4584 14.2141 12.5146C14.2868 12.5708 14.356 12.6311 14.4204 12.6962C14.4252 12.6894 14.4307 12.6825 14.4355 12.6756C14.4389 12.6722 14.4417 12.6681 14.4437 12.664C15.2551 11.5462 15.7636 10.2318 15.9144 8.86255L15.6417 8.8324Z"
                  fill="#8EFF7B"
                />
                <path
                  d="M28.0027 23.3713L18.6056 30.8913C17.8305 31.5108 16.7004 31.384 16.0816 30.611L6.7627 18.9647L7.10124 15.9144L7.16155 15.3689L7.51586 12.1726L7.9805 7.9812L12.1726 8.44653L15.3689 8.80221C15.2284 10.0817 14.7528 11.3098 13.9928 12.3521C13.9907 12.3556 13.988 12.359 13.9859 12.3624C13.6775 12.1822 13.3184 12.0794 12.936 12.0794C11.7792 12.0794 10.841 13.0169 10.841 14.1737C10.841 14.393 10.8746 14.6048 10.9369 14.8028C10.9644 14.8905 10.9979 14.9762 11.0363 15.0584C11.0754 15.1421 11.1199 15.2222 11.1693 15.2997C11.5407 15.8822 12.1931 16.2687 12.936 16.2687C14.0928 16.2687 15.0303 15.3305 15.0303 14.1737C15.0303 13.5967 14.7973 13.0745 14.4204 12.6962C14.4252 12.6893 14.4307 12.6825 14.4355 12.6756C14.4389 12.6722 14.4416 12.6681 14.4437 12.664C15.2551 11.5462 15.7636 10.2318 15.9144 8.86252L18.964 9.20175L20.7459 11.4283L28.283 20.848C28.9018 21.621 28.7777 22.7518 28.0027 23.3713Z"
                  fill="#8EFF7B"
                />
                <g opacity="0.2">
                  <path
                    d="M28.4625 22.821C28.3515 23.0273 28.1973 23.2158 28.0027 23.3713L18.6056 30.8913C17.8305 31.5108 16.7004 31.384 16.0816 30.611L6.7627 18.9647L7.10124 15.9144L7.16155 15.3689L7.51586 12.1726L7.9805 7.9812L8.783 8.07029L8.42938 11.2591L8.07507 14.4554L8.01476 15.0009L7.67622 18.0512L16.9951 29.6975C17.614 30.4705 18.7441 30.5973 19.5191 29.9778L28.4625 22.821Z"
                    fill="#303C44"
                  />
                </g>
                <path
                  d="M15.9624 7.98118C15.9624 8.27518 15.9459 8.57192 15.9144 8.86249C15.7636 10.2318 15.2551 11.5462 14.4437 12.6639C14.4416 12.668 14.4389 12.6722 14.4355 12.6756C14.4307 12.6824 14.4252 12.6893 14.4204 12.6961C13.5843 13.8372 12.4611 14.737 11.1693 15.2997C11.1199 15.2222 11.0754 15.142 11.0363 15.0584C10.9979 14.9762 10.9644 14.8905 10.9369 14.8028C12.1486 14.2779 13.2019 13.4349 13.9845 12.3645C13.9852 12.3638 13.9852 12.3631 13.9859 12.3624C13.988 12.359 13.9907 12.3555 13.9928 12.3521C14.7528 11.3098 15.2284 10.0817 15.3689 8.80219C15.399 8.53149 15.4141 8.2553 15.4141 7.98118C15.4141 3.88231 12.08 0.548252 7.98118 0.548252C3.88299 0.548252 0.548252 3.88231 0.548252 7.98118C0.548252 11.7799 3.39094 14.9556 7.16154 15.3689L7.10123 15.9144C3.05308 15.4703 0 12.0602 0 7.98118C0 3.58009 3.58009 0 7.98118 0C12.3823 0 15.9624 3.58009 15.9624 7.98118Z"
                  fill="#303C44"
                />
              </svg>
            </div>
            <button
              class="bank-btn autoschool-general_item-btn"
              @click="startExam('practic')"
            >
      Start the exam
            </button>
          </div>
          <div
            class="autoschool-general_item-lock"
            v-if="!theoryList[typeExam]"
          >
            <img src="/img/autoSchool/general/lock.png" alt="" />
            <div>
Before you pass a practical exam, you must successfully
theoretically pass
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { mapState, mapMutations, mapGetters } from "vuex";

export default {
  props: {
    typeExam: Number,
  },
  computed: {
    ...mapGetters("localization", ["loc"]),
    ...mapState("autoSchool", [
      "answersOnQuestions",
      "practicResults",
      "currentSchoolWelcome",
      "theoryPrice",
      "practicPrice",
      "theoryList",
      "practicePriceList"
    ]),
  },

  methods: {
    ...mapMutations("autoSchool", ["setCurrentSection"]),

    startExam: function (typeExam) {
      window.mp.trigger("school:startExam", typeExam);
    },

    showResult: function (typeResult) {
      this.setCurrentSection({ section: "ResultSection", data: typeResult });
    },

    getType(){
      return this.typeExam < 3 ? "Bodentransport" : this.typeExam === 3 ? "Water Transport ":" Air Transport "
    }
  },
};
</script>

<style lang="scss">
@function conv($px, $direction: false) {
  @return ($px / 20) + rem;
}

.autoschool {
  * {
    font-family: "Akrobat";
  }

  &-general {
    display: grid;
    grid-template-columns: conv(800) conv(884);
    column-gap: conv(65);
    padding-top: conv(127);

    &_left {
      display: flex;
      flex-direction: column;
      align-items: center;

      .autoschool-title {
        margin-bottom: conv(10);
      }

      img {
        margin-top: conv(41);
        width: 100%;
        height: auto;

        &.airplane {
          width: conv(844);
          margin-top: conv(39);
        }
      }
    }

    &_right {
      padding-top: conv(17);
      display: flex;
      flex-direction: column;
      align-items: center;

      &-list {
        width: 100%;
        display: grid;
        grid-template-columns: 1fr 1fr;
        column-gap: conv(30);
        height: conv(579);
      }
    }

    &_title {
      position: relative;
      margin-bottom: conv(58);
      font-weight: 800;
      font-size: conv(40);
      line-height: conv(48);
      text-align: center;
      color: #ffffff;
      display: flex;

      &::after {
        white-space: nowrap;
        content: "Exam selection";
        position: absolute;
        top: 50%;
        left: 50%;
        transform: translate(-50%, -50%);
        z-index: 4;
        font-weight: 800;
        font-size: conv(85);
        line-height: conv(102);
        text-align: center;
        color: rgba(255, 255, 255, 0.02);
      }
    }

    &_item {
      width: 100%;
      height: 100%;
      display: flex;
      flex-direction: column;
      position: relative;
      background: linear-gradient(
        0deg,
        rgba(255, 255, 255, 0.05) 0%,
        rgba(255, 255, 255, 0) 100%
      );

      &.lock {
        .autoschool-general_item-title,
        .autoschool-general_item-img,
        .autoschool-general_item-footer {
          opacity: 0.04;
        }
      }

      &-title {
        margin-bottom: conv(10);
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        display: flex;
        align-items: center;
        text-align: center;
        color: #ffffff;
        display: flex;
        align-items: center;
        justify-content: center;
        background: rgba(255, 255, 255, 0.05);
        height: conv(66);
        width: 100%;
      }

      &-img {
        width: 100%;
        height: conv(272);

        img {
          width: 100%;
          height: 100%;
        }
      }

      &-footer {
        height: conv(231);
        padding-top: conv(44);
        display: flex;
        align-items: center;
        flex-direction: column;
        width: 100%;
      }

      &-price {
        display: flex;
        position: relative;
        justify-content: center;
        align-items: center;
        width: conv(235);
        height: conv(57);
        background: rgba(255, 255, 255, 0.05);
        font-weight: 700;
        font-size: conv(16);
        line-height: conv(19);
        text-transform: uppercase;
        color: #ffffff;

        svg {
          position: absolute;
          top: conv(6);
          right: conv(-20.68);
          width: conv(29);
          height: conv(32);
        }
      }

      &-btn {
        width: conv(310);
        height: conv(75);
        display: flex;
        align-items: center;
        justify-content: center;
        font-weight: 700;
        font-size: conv(24);
        line-height: conv(29);
        text-transform: uppercase;
        color: #ffffff;
        cursor: pointer;
        margin-top: conv(-4);
        background: linear-gradient(
          180deg,
          rgba(71, 44, 132, 0.25) 0%,
          rgba(75, 0, 130, 0.25) 100%
        );
        outline: 1px solid #301934 ;
        box-shadow: inset 0px 0px 0.789rem rgba(75, 0, 130, 0.86);
        &:hover {
          box-shadow: inset 0px 0px 3.789rem rgba(75, 0, 130, 0.86);
          background: linear-gradient(
            180deg,
            rgba(71, 44, 132, 0.35) 0%,
            rgba(75, 0, 130, 0.35) 100%
          );
        }
      }

      &-lock {
        position: absolute;
        top: 0;
        left: 0;
        width: 100%;
        height: 100%;
        background: rgba(255, 255, 255, 0.1);
        z-index: 6;
        display: flex;
        justify-content: center;
        align-items: center;
        flex-direction: column;
        padding: 0 conv(55);

        img {
          height: conv(146);
          width: conv(146);
          margin-bottom: conv(24);
          display: block;
        }

        div {
          font-weight: 700;
          font-size: conv(20);
          line-height: conv(24);
          display: flex;
          align-items: center;
          text-align: center;
          text-transform: uppercase;
          color: #ffffff;
        }
      }
    }
  }

  &-title {
    display: flex;
    align-items: center;
    font-weight: 800;
    font-size: conv(64);
    line-height: conv(77);
    color: #ffffff;

    span {
      display: block;
      background: linear-gradient(
        180deg,
        #301934  0%,
        #ea0505 49.48%,
        #301934  100%
      );
      -webkit-background-clip: text;
      -webkit-text-fill-color: transparent;
      background-clip: text;
      text-fill-color: transparent;
      text-shadow: 0px 0px conv(49) rgba(255, 0, 0, 0.52);
    }
  }

  &-descr {
    font-weight: 500;
    font-size: conv(32);
    line-height: conv(38);
    text-align: center;
    color: #ffffff;
  }
}
</style>