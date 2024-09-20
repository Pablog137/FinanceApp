export default function LandingPage() {
  return (
    <header className="p-6 grid grid-cols-12">
      <div className="col-span-12 flex items-center gap-5">
        <img
          src="/images/logo.svg"
          alt="image"
          style={{ width: "50px", height: "50px" }}
        />
        <h1 className="text-3xl text-white font-bold">FinanceApp</h1>
      </div>
    </header>
  );
}
